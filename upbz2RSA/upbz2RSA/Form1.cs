using System;
using System.ComponentModel;
using System.Linq;
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

        DateTime start;
        DateTime finish;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(DecrFolder);
                Directory.CreateDirectory(EncrFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot create directories .\\Encrypt or .\\Decrypt\r\n" + ex.Message);
            }
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
            if (backgroundWorker1.IsBusy != true)
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
                            progressBar1.Maximum = 128;

                            // Start the asynchronous operation.
                            backgroundWorker1 = new BackgroundWorker();
                            backgroundWorker1.WorkerReportsProgress = true;
                            backgroundWorker1.WorkerSupportsCancellation = false;
                            backgroundWorker1.DoWork += (obj, dwe) => EncryptFile(name, ext);
                            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(updateGui);
                            backgroundWorker1.RunWorkerAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("SOMETHING WENT WRONG \r\n(BackgroundWorker / EncryptFile)");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Background Process is busy");
            }
        }

        private void updateGui(object sender, ProgressChangedEventArgs e)
        {
            // ProgressPercentage == 1 indicates:
            // increment progressBar1 value
            if (e.ProgressPercentage == 1)
                progressBar1.Increment(1);
            // ProgressPercentage == 0 indicates:
            // we set only label
            else if (e.ProgressPercentage == 0)
            {
                // set label
                if (e.UserState != null)
                    label1.Text = (string)e.UserState;
            }
            // ProgressPercentage == 100 indicates:
            // render 100% full progress bar
            // relevant only because of very small files 
            else if (e.ProgressPercentage == 100)
                progressBar1.Value = progressBar1.Maximum;
            else if (e.ProgressPercentage == -1)
                progressBar1.Value = 0;
        }

        public void Logger(String lines)
        {
            StreamWriter file = new StreamWriter(Directory.GetCurrentDirectory() + @"\log.txt", true);
            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log
            try
            {
                file.WriteLine(lines);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fatal: Logger failed. " + ex.Message);
            }
            finally
            {
                if(file != null)
                    file.Close();
            }
        }

        private void EncryptFile(string inFile, string ext)
        {
            start = DateTime.Now;
            Logger("Enc: Started on " + start.ToString("dd.MM.yyyy 'at' HH:mm:ss"));

            int startFileName = inFile.LastIndexOf("\\") + 1;
            string shortFileName = inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + ext;
            // Change the file's extension to ".enc"
            string outFile = EncrFolder + inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + "C" + ext;
            // make sure directory exist
            try { 
                Directory.CreateDirectory(EncrFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot create directory .\\Encrypt\r\n" + ex.Message);
                return;
            }

            string labelText = "Encrypting file \"" + shortFileName + "\"";
            backgroundWorker1.ReportProgress(0, labelText);
            
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

            // prepare 32B dummyHash
            byte[] dummyHash = new byte[32];
            byte[] pad = BitConverter.GetBytes(9);

            // Write the following to the FileStream
            // for the encrypted file (outFs):
            // - length of the key
            // - length of the IV
            // - ecrypted key
            // - the IV
            // - dummyHash to check integrity
            // - the encrypted cipher content
            // --- rewrite dummyHash with true hash of encrypted data

            try {
                using (FileStream outFs = new FileStream(outFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 8388608))
                {
                    outFs.Write(LenK, 0, 4);
                    outFs.Write(LenIV, 0, 4);
                    outFs.Write(keyEncrypted, 0, lKey);
                    outFs.Write(rjndl.IV, 0, lIV);

                    // write dummyHash
                    outFs.Write(dummyHash, 0, dummyHash.Length);

                    // Now write the cipher text using
                    // a CryptoStream for encrypting.
                    using (CryptoStream outStreamEncrypted = new NotClosingCryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {

                        // By encrypting a chunk at
                        // a time, you can save memory
                        // and accommodate large files.
                        int count = 0;
                        long offset = 0;

                        // blockSizeBytes can be any arbitrary size.
                        int blockSizeBytes = rjndl.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];
                        int bytesRead = 0;

                        using (FileStream inFs = new FileStream(inFile, FileMode.Open, FileAccess.Read, FileShare.Read, 8388608))
                        {
                            double fileLength = inFs.Length;
                            double blocks = fileLength / blockSizeBytes;
                            int step = (int)Math.Ceiling(blocks) / 128;
                            if (step < 1)
                                step = 1;
                            int iteration = 1;
                            do
                            {
                                count = inFs.Read(data, 0, blockSizeBytes);
                                offset += count;
                                outStreamEncrypted.Write(data, 0, count);
                                bytesRead += blockSizeBytes;

                                if (iteration++ % step == 0)
                                    backgroundWorker1.ReportProgress(1);    //increment progressBar and NOT change label1
                            }
                            while (count > 0);

                            backgroundWorker1.ReportProgress(100);  //set 100% full progress bar due to very small files

                            Logger("Enc: Original file length = " + fileLength);   //dlzka cisteho suboru

                            inFs.Close();
                        } // FileStream inFs
                    } // CryptoStream outStreamEncrypted

                    labelText = "Computing HMAC hash";
                    backgroundWorker1.ReportProgress(0, labelText);

                    // generate initialization key for HMACSHA256 provider
                    byte[] hashKey = new byte[lIV + 4];
                    rjndl.IV.CopyTo(hashKey, 0);
                    pad.CopyTo(hashKey, (hashKey.Length - 4));

                    // make and reWrite hash
                    using (HMACSHA256 hmac = new HMACSHA256(hashKey))
                    {
                        // set position in stream
                        long dataIndex = LenK.Length + LenIV.Length + lKey + lIV + dummyHash.Length;
                        outFs.Seek(dataIndex, SeekOrigin.Begin);

                        // generate hash
                        dummyHash = hmac.ComputeHash(outFs);

                        Logger("Enc: hash = " + BitConverter.ToString(dummyHash));

                        // write hash
                        dataIndex -= dummyHash.Length;
                        outFs.Seek(dataIndex, SeekOrigin.Begin);
                        outFs.Write(dummyHash, 0, dummyHash.Length);

                        //erase hash from memory
                        for (int i = 0; i < 32; i += 4)
                        {
                            pad.CopyTo(dummyHash, i);
                        }
                    }//HMACSHA256

                    Logger("Enc: Encrypted file length = " + outFs.Length);

                    labelText = "Encryption was succesful";
                    backgroundWorker1.ReportProgress(0, labelText);

                    outFs.Close();
                    Logger("Enc: Encryption was succesful");

                    finish = DateTime.Now;
                    TimeSpan duration = finish.Subtract(start);
                    Logger("Enc: Finished on " + finish.ToString("dd.MM.yyyy 'at' HH:mm:ss"));
                    Logger("File \"" + inFile + "\"\r\n     encrypted in " +
                            duration.Days + " days, " +
                            duration.Hours + ":" +
                            duration.Minutes + ":" + duration.Seconds +
                            "." + duration.Milliseconds +
                            "\r\n=================================================="
                    );
                }//outFs
            }
            catch(Exception exc)
            {
                backgroundWorker1.ReportProgress(-1);
                backgroundWorker1.ReportProgress(0, "Error occured in encrypt process");
                MessageBox.Show("Error occured in encrypt process\r\n" + exc.GetType() + "\r\n" + exc.Message);
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
            if (backgroundWorker1.IsBusy != true)
            {
                if (rsa == null)
                    MessageBox.Show("Key not set.");
                else if (rsa.PublicOnly)
                    MessageBox.Show("Only public key was set\r\nYou need right private key to decrypt file");
                else
                {
                    if (fName != null)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(fName);
                            string name = fi.FullName;
                            progressBar1.Value = 0;
                            progressBar1.Maximum = 128;

                            // Start the asynchronous operation.
                            backgroundWorker1 = new BackgroundWorker();
                            backgroundWorker1.WorkerReportsProgress = true;
                            backgroundWorker1.WorkerSupportsCancellation = false;
                            backgroundWorker1.DoWork += (obj, dwe) => DecryptFile(name, ext);
                            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(updateGui);
                            backgroundWorker1.RunWorkerAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("SOMETHING WENT WRONG\r\n(BackgroundWorker / DecryptFile)");
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Background Process is busy");
            }
        }
        private void DecryptFile(string inFile, string ext)
        {
            start = DateTime.Now;
            Logger("Dec: Started on " + start.ToString("dd.MM.yyyy 'at' HH:mm:ss"));

            int startFileName = inFile.LastIndexOf("\\") + 1;
            string shortFileName = inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + ext;
            //string shortFileName = inFile.Substring(0, inFile.LastIndexOf(".")) + ext;
            string labelText = "Decrypting file \"" + shortFileName + "\"";

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
            string outFile = DecrFolder + shortFileName.Substring(0, shortFileName.LastIndexOf(".")-1) + ext;
            // make sure directory exist
            try { 
                Directory.CreateDirectory(DecrFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot create directory .\\Decrypt\r\n" + ex.Message);
                return;
            }

            // Use FileStream objects to read the encrypted
            // file (inFs) and save the decrypted file (outFs).
            try {
                using (FileStream inFs = new FileStream(inFile, FileMode.Open, FileAccess.Read, FileShare.Read, 8388608))
                {
                    inFs.Seek(0, SeekOrigin.Begin);
                    inFs.Seek(0, SeekOrigin.Begin);
                    inFs.Read(LenK, 0, 3);
                    inFs.Seek(4, SeekOrigin.Begin);
                    inFs.Read(LenIV, 0, 3);

                    // Convert the lengths to integer values.
                    int lenK = BitConverter.ToInt32(LenK, 0);
                    int lenIV = BitConverter.ToInt32(LenIV, 0);
                    int lenHash = 32;

                    // Create the byte arrays for
                    // the encrypted Rijndael key,
                    // the IV, and the hash.
                    byte[] KeyEncrypted = new byte[lenK];
                    byte[] IV = new byte[lenIV];
                    byte[] hashRead = new byte[lenHash];

                    // Extract the key, IV and hash
                    // starting from index 8
                    // after the length values.
                    inFs.Seek(8, SeekOrigin.Begin);
                    inFs.Read(KeyEncrypted, 0, lenK);
                    inFs.Seek(8 + lenK, SeekOrigin.Begin);
                    inFs.Read(IV, 0, lenIV);
                    inFs.Seek(8 + lenK + lenIV, SeekOrigin.Begin);
                    inFs.Read(hashRead, 0, lenHash);

                    Logger("Dec: hash declared = " + BitConverter.ToString(hashRead));

                    // check hash
                    // construct key for HMACSHA provider
                    byte[] hashKey = new byte[lenIV + 4];
                    IV.CopyTo(hashKey, 0);
                    byte[] pad = BitConverter.GetBytes(9);
                    pad.CopyTo(hashKey, (hashKey.Length - 4));

                    // make and compare hash
                    using (HMACSHA256 hmac = new HMACSHA256(hashKey))
                    {
                        // set position in stream
                        long dataIndex = LenK.Length + LenIV.Length + lenK + lenIV + lenHash;
                        inFs.Seek(dataIndex, SeekOrigin.Begin);

                        labelText = "Checking file integrity, please wait...";
                        backgroundWorker1.ReportProgress(0, labelText);

                        // generate hash
                        byte[] computedHash = hmac.ComputeHash(inFs);

                        Logger("Dec: hash computed = " + BitConverter.ToString(computedHash));

                        try
                        {
                            bool isHashOK = Enumerable.SequenceEqual(hashRead, computedHash);
                            if (!isHashOK)
                            {
                                backgroundWorker1.ReportProgress(-1);
                                Logger("Dec: hashes does not match");
                                labelText = "Cannot decrypt file. Integrity check ended up with false";
                                backgroundWorker1.ReportProgress(0, labelText);
                                throw new Exception("Cannot guarantee file integrity.\r\n" +
                                                    "Integrity check ended up with false"
                                );
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            return;
                        }

                        Logger("Dec: hash OK");

                        labelText = "Decrypting file \"" + shortFileName + "\"";
                        backgroundWorker1.ReportProgress(0, labelText);
                    }

                    // Use RSACryptoServiceProvider
                    // to decrypt the Rijndael key.
                    byte[] KeyDecrypted;
                    try {
                        KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);
                    }
                    catch(CryptographicException ce)
                    {
                        backgroundWorker1.ReportProgress(0, "Cannot decrypt file \"" + shortFileName + "\" with given key.");
                        inFs.Dispose();
                        backgroundWorker1.ReportProgress(-1);
                        MessageBox.Show("Data integrity is OK, but cannot decrypt!\r\nYou have probably wrong key!\r\nOfficial cause: " + ce.Message);
                        return;
                    }

                    // Decrypt the key.
                    ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);

                    // Determine the start postition of
                    // the ciphter text (startC)
                    // and its length(lenC).
                    int startC = LenK.Length + LenIV.Length + lenK + lenIV + lenHash;
                    int lenC = (int)inFs.Length - startC;

                    // Decrypt the cipher text from
                    // from the FileSteam of the encrypted
                    // file (inFs) into the FileStream
                    // for the decrypted file (outFs).
                    using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                    {
                        int count = 0;

                        // blockSizeBytes can be any arbitrary size.
                        int blockSizeBytes = rjndl.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];

                        // By decrypting a chunk a time,
                        // you can save memory and
                        // accommodate large files.

                        // Start at the beginning
                        // of the cipher text.
                        inFs.Seek(startC, SeekOrigin.Begin);
                        using (CryptoStream outStreamDecrypted = new NotClosingCryptoStream(outFs, transform, CryptoStreamMode.Write))
                        {
                            double fileLength = lenC;//inFs.Length;
                            double blocks = fileLength / blockSizeBytes;
                            int step = (int)Math.Ceiling(blocks) / 128;
                            if (step < 1)
                                step = 1;
                            int iteration = 1;

                            do
                            {
                                count = inFs.Read(data, 0, blockSizeBytes);
                                outStreamDecrypted.Write(data, 0, count);

                                if (iteration++ % step == 0)
                                    backgroundWorker1.ReportProgress(1);    //increment progressBar and NOT change label1
                            }
                            while (count > 0);

                            backgroundWorker1.ReportProgress(100);  //set 100% full progress bar due to very small files
                        }//CryptoStream

                        Logger("Dec: Decrypted file length = " + outFs.Length);

                        outFs.Close();
                    }//outFs
                    inFs.Close();

                    labelText = "Decryption of \"" + shortFileName + "\" was succesful";
                    backgroundWorker1.ReportProgress(0, labelText);

                    Logger("Dec: Decryption was succesful");
                    finish = DateTime.Now;

                    TimeSpan duration = finish.Subtract(start);
                    Logger("Dec: Finished on " + finish.ToString("dd.MM.yyyy 'at' HH:mm:ss"));
                    Logger("File \"" + inFile + "\"\r\n     decrypted in " +
                            duration.Days + " days, " +
                            duration.Hours + ":" +
                            duration.Minutes + ":" + duration.Seconds +
                            "." + duration.Milliseconds +
                            "\r\n=================================================="
                    );
                }
            }
            catch(Exception exc)
            {
                backgroundWorker1.ReportProgress(-1);
                backgroundWorker1.ReportProgress(0, "Error occured in decrypt process");
                MessageBox.Show("Error occured in decrypt process"  + exc.GetType() + "\r\n" + exc.Message);
            }

        }

        private void exprtPubKeyBtn_Click(object sender, EventArgs e)
        {
            // Save the public key created by the RSA
            // to a file. Caution, persisting the
            // key to a file is a security risk.
            Directory.CreateDirectory(EncrFolder);
            try
            {
                StreamWriter sw = new StreamWriter(PubKeyFile, false);
                try {
                    sw.Write(rsa.ToXmlString(false));
                    sw.Close();
                    label1.Text = "Public key was succesfully exported to file";
                }
                catch(Exception exc) {
                    sw.Close();
                    File.Delete(PubKeyFile);
                    MessageBox.Show("Cannot export Public key\r\nGenerate or import key first");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to create file\r\n" + PubKeyFile);
            }
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
                MessageBox.Show("Cannot import Public Key\r\n" + ex.Message);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e) { }

        private void exprtPrivKey_Click(object sender, EventArgs e)
        {
            // Save the private key created by the RSA
            // to a file. Caution, persisting the
            // key to a file is a security risk.
            Directory.CreateDirectory(DecrFolder);
            try
            {
                StreamWriter sw = new StreamWriter(PrivateKeyFile, false);
                try
                {
                    sw.Write(rsa.ToXmlString(true));
                    sw.Close();
                    label1.Text = "Private key was succesfully exported to file";
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Cannot export Private key\r\nGenerate or import key first");
                    sw.Close();
                    File.Delete(PrivateKeyFile);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to create file\r\n" + PrivateKeyFile);
            }
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
                MessageBox.Show("Cannot import Private Key\r\n" + ex.Message);
            }
        }

        private void passtxt_TextChanged(object sender, EventArgs e)
        {
            keyName = passtxt.Text;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void slctFileTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
