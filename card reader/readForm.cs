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
using PCSC;
using PCSC.Iso7816;
namespace card_reader
{
    public partial class readform : Form
    {

        public readform()
        {
            InitializeComponent();
        }

        private void Readbutton_Click(object sender, EventArgs e)
        {
            string id = Readcardid();
          if (id != "") viewer.Items.Add(id);
           else viewer.Items.Add("no data");

        }
        string Readcardid()
        {
            SCardContext card = new SCardContext();

            card.Establish(SCardScope.System);
            string[] readers = card.GetReaders();
            SCardReaderState status = card.GetReaderStatus(readers[0]);
            return BitConverter.ToString(status.Atr ?? new byte[0]);
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
                string filePath = @"";

                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                }

                if(filePath != @"")
                {
                    StreamWriter sw = new StreamWriter(filePath);
                    foreach (string cardid in viewer.Items)
                    {
                    sw.WriteLine(cardid);
                    }
                sw.Close();
                }
        }

        private void Readform_Load(object sender, EventArgs e)
        {

        }
    }
}
