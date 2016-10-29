using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace upbz2RSA
{
    public partial class Form1 : Form
    {

        // Declare CspParmeters and RsaCryptoServiceProvider
        // objects with global scope of your Form class.
        CspParameters cspp = new CspParameters();
        RSACryptoServiceProvider rsa;

        // Path variables for source, encryption, and
        // decryption folders. Must end with a backslash.
         string EncrFolder = Directory.GetCurrentDirectory() + @"\Encrypt\";
         string DecrFolder = Directory.GetCurrentDirectory() + @"\Decrypt\";
         string SrcFolder = Directory.GetCurrentDirectory();
         string fName;
         string ext;

        // Public key file
         string PubKeyFile = Directory.GetCurrentDirectory() + @"\Encrypt\rsaPublicKey.txt";
        string PrivateKeyFile= Directory.GetCurrentDirectory() + @"\Decrypt\rsaPrivateKey.txt";
        // Key container name for
        // private/public key value pair.
         string keyName = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void crtAsymKeyBtn_Click(object sender, EventArgs e)
        {
            // Stores a key pair in the key container.
            if (passtxt.Text!="") {
                keyName = passtxt.Text;
                cspp.KeyContainerName = keyName;
                rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;
                if (rsa.PublicOnly == true)
                    label1.Text = "Key: " + cspp.KeyContainerName + " - Public Only";
                else
                    label1.Text = "Key: " + cspp.KeyContainerName + " - Full Key Pair";
            }
            else {
                MessageBox.Show("Insert password to generate key pair");
            }
        }
        private void SlctFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = SrcFolder;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 fName = openFileDialog1.FileName;
                 ext = Path.GetExtension(openFileDialog1.FileName);
                 slctFileTxt.Text = fName;
            }
        }

        private void EncBtn_Click(object sender, EventArgs e)
        {
            if (rsa == null)
                MessageBox.Show("Key not set.");
            else
            {

                    if (fName != null)
                    {
                        try
                        {
                            FileInfo fInfo = new FileInfo(fName);
                            // Pass the file name without the path.
                            string name = fInfo.FullName;
                            progressBar1.Value = 0;
                            EncryptFile(name, ext);
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("SOMETHING WENT WRONG");
                        }
                }
            }
        }
     

        private void EncryptFile(string inFile, string ext)
        {

            label1.Text = "";
            // Create instance of Rijndael for
            // symetric encryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC;
            ICryptoTransform transform = rjndl.CreateEncryptor();

            // Use RSACryptoServiceProvider to
            // enrypt the Rijndael key.
            // rsa is previously instantiated: 
            //    rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = rsa.Encrypt(rjndl.Key, false);

            // Create byte arrays to contain
            // the length values of the key and IV.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            int lKey = keyEncrypted.Length;
            LenK = BitConverter.GetBytes(lKey);
            int lIV = rjndl.IV.Length;
            LenIV = BitConverter.GetBytes(lIV);

            // Write the following to the FileStream
            // for the encrypted file (outFs):
            // - length of the key
            // - length of the IV
            // - ecrypted key
            // - the IV
            // - the encrypted cipher content

            int startFileName = inFile.LastIndexOf("\\") + 1;
            // Change the file's extension to ".enc"
            string outFile = EncrFolder + inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName)+"C" + ext;

            using (FileStream outFs = new FileStream(outFile, FileMode.Create))
            {

                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(rjndl.IV, 0, lIV);

                // Now write the cipher text using
                // a CryptoStream for encrypting.
                using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {

                    // By encrypting a chunk at
                    // a time, you can save memory
                    // and accommodate large files.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;

                    using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                    {
                        double fileLength = inFs.Length;
                        double blocks = fileLength / blockSizeBytes;
                        progressBar1.Maximum = (int)Math.Ceiling(blocks);
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;
                            progressBar1.Increment(1);
                        }
                        while (count > 0);
                        inFs.Close();
                    }
                    outStreamEncrypted.FlushFinalBlock();
                    outStreamEncrypted.Close();
                }
                if (progressBar1.Value == progressBar1.Maximum)
                {
                    label1.Text = "Encryption was succesful";
                }
                outFs.Close();
            }
           
        }

        private void slcFileDbtn_Click(object sender, EventArgs e)
        {
            // Display a dialog box to select the encrypted file.
            openFileDialog2.InitialDirectory = EncrFolder;
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                fName = openFileDialog2.FileName;
                ext = Path.GetExtension(openFileDialog2.FileName);
                stcFileDtxt.Text = fName;
            }
        }

        private void dcrtBtn_Click(object sender, EventArgs e)
        {
            if (rsa == null)
                MessageBox.Show("Key not set.");
            else
            {              
                    if (fName != null)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(fName);
                            string name = fi.Name;
                            progressBar1.Value = 0;
                            DecryptFile(name, ext);
                            
                        }
                        catch (Exception ex) {
                            MessageBox.Show("SOMETHING WENT WRONG"+ ex.Message);
                        }
                    }
                
            }
        }
        private void DecryptFile(string inFile, string ext)
        {

            label1.Text = "";
            // Create instance of Rijndael for
            // symetric decryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC;

            // Create byte arrays to get the length of
            // the encrypted key and IV.
            // These values were stored as 4 bytes each
            // at the beginning of the encrypted package.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Consruct the file name for the decrypted file.
            string outFile = DecrFolder + inFile.Substring(0, inFile.LastIndexOf(".")-1) + ext;

            // Use FileStream objects to read the encrypted
            // file (inFs) and save the decrypted file (outFs).
            using (FileStream inFs = new FileStream(EncrFolder + inFile, FileMode.Open))
            {

                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Convert the lengths to integer values.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                // Determine the start postition of
                // the ciphter text (startC)
                // and its length(lenC).
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Create the byte arrays for
                // the encrypted Rijndael key,
                // the IV, and the cipher text.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Extract the key and IV
                // starting from index 8
                // after the length values.
                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);
                Directory.CreateDirectory(DecrFolder);
                // Use RSACryptoServiceProvider
                // to decrypt the Rijndael key.
                byte[] KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);

                // Decrypt the key.
                ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);

                // Decrypt the cipher text from
                // from the FileSteam of the encrypted
                // file (inFs) into the FileStream
                // for the decrypted file (outFs).
                using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                {

                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];


                    // By decrypting a chunk a time,
                    // you can save memory and
                    // accommodate large files.

                    // Start at the beginning
                    // of the cipher text.
                    inFs.Seek(startC, SeekOrigin.Begin);
                    using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        double fileLength = inFs.Length;
                        double blocks = fileLength / blockSizeBytes;
                        progressBar1.Maximum = (int)Math.Ceiling(blocks-(blockSizeBytes));

                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            progressBar1.Increment(1);
                            outStreamDecrypted.Write(data, 0, count);

                        }
                        while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                        outStreamDecrypted.Close();
                    }
                    
                    outFs.Close();
                }
                inFs.Close();
                if (progressBar1.Value == (progressBar1.Maximum))
                {
                    label1.Text = "Decryption was succesful";
                }
            }
            

        }


        private void exprtPubKeyBtn_Click(object sender, EventArgs e)
        {
            // Save the public key created by the RSA
            // to a file. Caution, persisting the
            // key to a file is a security risk.
            Directory.CreateDirectory(EncrFolder);
            StreamWriter sw = new StreamWriter(PubKeyFile, false);
            sw.Write(rsa.ToXmlString(false));
            sw.Close();
            label1.Text = "Public key was succesfully exported to file";
        }

        private void ImprtPubKey_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(PubKeyFile);
                cspp.KeyContainerName = keyName;
                rsa = new RSACryptoServiceProvider(cspp);
                string keytxt = sr.ReadToEnd();
                rsa.FromXmlString(keytxt);
                rsa.PersistKeyInCsp = true;
                if (rsa.PublicOnly == true)
                    label1.Text = "Key: " + cspp.KeyContainerName + " - Container contains only public key";
                else
                    label1.Text = "Key: " + cspp.KeyContainerName + " - Container contains Full Key Pair";
                sr.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Cannot import Public Key");
            }
        }

        /*private void getPrvtKeyBtn_Click(object sender, EventArgs e)
        {
            /*  cspp.KeyContainerName = keyName;

              rsa = new RSACryptoServiceProvider(cspp);
              rsa.PersistKeyInCsp = true;

              if (rsa.PublicOnly == true)
                  label1.Text = "Key: " + cspp.KeyContainerName + " - Container contains only public key";
              else
                  label1.Text = "Key: " + cspp.KeyContainerName + " - Container contains Full Key Pair";
        }*/

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void exprtPrivKey_Click(object sender, EventArgs e)
        {
            // Save the private key created by the RSA
            // to a file. Caution, persisting the
            // key to a file is a security risk.
            Directory.CreateDirectory(DecrFolder);
            StreamWriter sw = new StreamWriter(PrivateKeyFile, false);
            sw.Write(rsa.ToXmlString(true));
            sw.Close();
            label1.Text = "Private key was succesfully exported to file";
        }

        private void imprPrivKeyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(PrivateKeyFile);
                cspp.KeyContainerName = keyName;
                rsa = new RSACryptoServiceProvider(cspp);
                string keytxt = sr.ReadToEnd();
                rsa.FromXmlString(keytxt);
                rsa.PersistKeyInCsp = true;
                if (rsa.PublicOnly == true)
                    label1.Text = "Key: " + cspp.KeyContainerName + " - Container contains only public key";
                else
                    label1.Text = "Key: " + cspp.KeyContainerName + " - Container contains Full Key Pair";
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot import Private Key" + ex.Message);
            }
        }
    }
}
