using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTeam_Gen
{
    public partial class Form1 : Form
    {
        public static byte[] key = new byte[24];
        public static byte[] iv = new byte[16];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(srcText.Text.Length == 0)
            {
                MessageBox.Show("老铁，请先输入原始的shellcode。");
            }
            else
            {
                this.tob64();
            }
        }

        private void genAesKey(byte[] data,String secret)
        {
            /*
            int bar = data.Length / 4 + data.Length / 2;

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = data[bar + i];

                if (i < iv.Length)
                {
                    iv[i] = data[bar + i + 24];
                }
            }
            */
            String _str = "abcdefghijklmnopqrstuvwxyz;?/][}{+-()*&^%$#@!~,.";
            Random r = new Random();
            for(int i=0;i < key.Length; i++)
            {
                key[i] = (byte)_str[r.Next(_str.Length)];

                if (i < iv.Length)
                {
                    iv[i] = (byte)_str[r.Next(_str.Length)];
                }
            }
            
            
            //Environment.Exit(0);
            String encrypted = EncryptStringToBytes_Aes(secret, key, iv);
            dstText.Text = encrypted + ":" + System.Text.Encoding.UTF8.GetString(key) + ":" + System.Text.Encoding.UTF8.GetString(iv);

            //Environment.Exit(0);
        }
        public void tob64()
        {
            String tmp = srcText.Text.Split('{')[1].Split('}')[0];
            String[] result = tmp.Split(',');
            byte[] data = new byte[result.Length];
            try
            {


                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = byte.Parse(result[i].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                }


                String b64 = Convert.ToBase64String(data);
                dstText.Text = b64;
                this.genAesKey(data, b64);
            }catch(Exception e)
            {
                MessageBox.Show("请输cs原始完整的shellcode。","Warning");
                Console.WriteLine(e);
            }
        }

        static String EncryptStringToBytes_Aes(String plainText, byte[] Key, byte[] Iv)
        {
            byte[] encrypted;

            using(Aes aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = Iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText,0,plainText.Length);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
    }
}
