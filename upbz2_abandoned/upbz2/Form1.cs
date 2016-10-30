using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace upbz2
{
    public partial class Form1 : Form
    {
        string fileName;
        string keyFile;
        string EncrFolder = Directory.GetCurrentDirectory()+@"\Encrypt\";
        string DecrFolder = Directory.GetCurrentDirectory()+@"\Decrypt\";
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) {
                    string path = openFileDialog1.FileName;
                    textBox1.Text = path;
                    fileName = openFileDialog1.FileName;


                }
               
                
            }
            
            catch(Exception ex){
                 MessageBox.Show("ERROR");
            }
        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    if (textBox2.Text != "")
                    {

                        if (textBox2.Text.Length >= 10)
                        {

                            //testLabel.Text = key;
                            //SaveFileStream(GenerateStreamFromString(inputTxt));
                            if (fileName != null)
                            {
                                String key = textBox2.Text;
                                FileInfo fInfo = new FileInfo(fileName);

                                //TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();
                                //TDES.
                                //key generation
                                byte[] genKey = generateKey(key);
                                
                                Stream keyStream=GenerateStreamFromBytes(genKey);
                                SaveFileStream(keyStream);
                                // Pass the file name without the path.
                                string ext = fInfo.Extension;
                                string name = fInfo.FullName; 
                                EncryptFile(name,ext,genKey);
                                testLabel.Text = "Šifrovanie prebehlo úspešne";
                                

                            }
                            else
                            {
                                testLabel.Text = "Prosím znova vyberte súbor";
                            }
                        }
                        else
                        {
                            testLabel.Text = "Kľúč musí mať aspoň 10 znakov";
                        }
                    }
                    else
                    {
                        testLabel.Text = "Zadajte kľúč";
                    }
                }
                else
                {
                    testLabel.Text = "Vyberte súbor";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR "+ ex.Message);
            }
        }

        private void SaveFileStream(Stream stream)
        {
            var fileStream = new FileStream(Directory.GetCurrentDirectory() + @"\" + "Key.txt", FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }

        public Stream GenerateStreamFromBytes(byte[] s)
        {
            return new MemoryStream(s);
        }

        /*private void decrBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    //testLabel.Text = Directory.GetCurrentDirectory() + @"\text.txt";
                    //SaveFileStream(GenerateStreamFromString(inputTxt));
                    if (fileName != null)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension;
                        string name = fi.Name;
                        byte[] keyDec = Encoding.Unicode.GetBytes(keyFile); ;
                        DecryptFile(name,ext,keyDec);
                    }
                    testLabel.Text = "Dešifrovanie prebehlo úspešne";
                }
                else {
                    testLabel.Text = "Zadajte kľúč";   
                }
            }
            else
            {
                testLabel.Text = "Vyberte súbor";

            }
        }*/

        private void EncryptFile(string inFile,string ext, byte[] newKey)
        {

            // Create instance of Rijndael for
            // symetric encryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            
            rjndl.KeySize = 192;
            rjndl.BlockSize = 192;//asi 128
            rjndl.Mode = CipherMode.CBC;
            ICryptoTransform transform = rjndl.CreateEncryptor();

            // Use RSACryptoServiceProvider to
            // enrypt the Rijndael key.
            // rsa is previously instantiated: 
            //    rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = newKey;//= rsa.Encrypt(rjndl.Key, false);
            ///////////////////////////////////////////////////////
            //KLUC TREBA DORATAT//////////////////////////////////
            ///////////////////////////////////////////////////////
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
            string outFile = EncrFolder + inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName)+"C"+ ext;

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
                        int blocks= Convert.ToInt32(inFs.Length/ blockSizeBytes);//inFs.Read(data, 0, blockSizeBytes);
                        progressBar1.Maximum = progressBar1.Size.Width;
                        progressBar1.Step = (progressBar1.Size.Width / blocks);
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
                outFs.Close();
            }

        }

        private void DecryptFile(string inFile, string ext, byte[] keyDec)
        {

            // Create instance of Rijndael for
            // symetric decryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            
            rjndl.KeySize = 192;
            rjndl.BlockSize = 192;
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
                byte[] KeyDecrypted = KeyEncrypted;//rsa.Decrypt(KeyEncrypted, false);
                    /////////////////////TREBA DORATAT KLUC////////////////////
                    ///////////////////////////////////////////////////////////

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
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamDecrypted.Write(data, 0, count);

                        }
                        while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                        outStreamDecrypted.Close();
                    }
                    outFs.Close();
                }
                inFs.Close();
            }

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

       
        public byte[] generateKey(string key)
        {
            byte[] pwd = Encoding.ASCII.GetBytes(key);

            byte[] salt = CreateRandomSalt(7);

            // Create a TripleDESCryptoServiceProvider object.
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            try
            {
                Console.WriteLine("Creating a key with PasswordDeriveBytes...");

                // Create a PasswordDeriveBytes object and then create
                // a TripleDES key from the password and salt.
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);


                // Create the key and set it to the Key property
                // of the TripleDESCryptoServiceProvider object.
                
                tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);


                Console.WriteLine("Operation complete.");
                return tdes.Key;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR " + e.Message);
                return null;
            }
            finally
            {
                // Clear the buffers
                ClearBytes(pwd);
                ClearBytes(salt);

                // Clear the key.
                tdes.Clear();
            }
        }
        public static byte[] CreateRandomSalt(int length)
        {
            // Create a buffer
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes;
        }

        public static void ClearBytes(byte[] buffer)
        {
            // Check arguments.
            if (buffer == null)
            {
                throw new ArgumentException("buffer");
            }

            // Set each byte in the buffer to 0.
            for (int x = 0; x < buffer.Length; x++)
            {
                buffer[x] = 0;
            }
        }

        private void slcKeyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;
                    textBox4.Text = path;
                    using (Stream myStream = openFileDialog1.OpenFile())
                    {
                        
                        StreamReader reader = new StreamReader(path);
                        keyFile = reader.ReadToEnd();
                    }
                }
               
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR");
            }
        }

        private void decrBtn_Click_1(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
               // if (textBox4.Text != "")
               // {
                    //testLabel.Text = Directory.GetCurrentDirectory() + @"\text.txt";
                    //SaveFileStream(GenerateStreamFromString(inputTxt));
                    if (fileName != null)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        string ext = fi.Extension;
                        string name = fi.Name;
                        byte[] keyDec = null;// Encoding.ASCII.GetBytes(keyFile);
                        DecryptFile(name, ext, keyDec);
                    }
                    label5.Text = "Dešifrovanie prebehlo úspešne";
               // }
               // else
               // {
               //     label5.Text = "Zadajte kľúč";
               // }
            }
            else
            {
                label5.Text = "Vyberte súbor";

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;
                    textBox3.Text = path;
                    fileName = openFileDialog1.FileName;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR");
            }
        }
    }
    
    
}
