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
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private List<string> forbiddenWords = new List<string>();
        private List<string> reportText = new List<string>();

        public Form1()
        {
            InitializeComponent();
            buttonLoad.Enabled = true;
            buttonStart.Enabled = false;
            buttonReport.Enabled = false;
            buttonStop.Enabled = false;
            buttonResume.Enabled = false;
            buttonPause.Enabled = false;
            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 0;
            textBoxTargetFolder.Text = "C:\\text";
            textBoxCopyFolder.Text = "C:\\Users\\" + Environment.UserName + "\\Desktop\\test";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            listBoxForbiddenWords.Items.Clear();
            forbiddenWords = new List<string>();
            reportText = new List<string>();

            // Открываем диалоговое окно выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Читаем файл с запрещенными словами
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        forbiddenWords.Add(reader.ReadLine());
                    }
                }

                // Обновляем список запрещенных слов
                for (int i = 0; i < forbiddenWords.Count; i++)
                {
                    listBoxForbiddenWords.Items.Add(forbiddenWords[i]);
                }
            }
            buttonStart.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(textBoxCopyFolder.Text + "\\report.txt"))
            {
                // Запишите в файл количество заменённых слов
                writer.Write("");
            }
            listBoxReport.Items.Clear();

            // Получаем пути к папкам с исходными и целевыми файлами
            string targetFolderPath = textBoxTargetFolder.Text;
            string copyFolderPath = textBoxCopyFolder.Text;

            // Создаем объект BackgroundWorker
            BackgroundWorker worker = new BackgroundWorker();

            // Устанавливаем свойство WorkerReportsProgress в true
            worker.WorkerReportsProgress = true;
            progressBar.Value = 10;

            // Задаем обработчик завершения работы
            worker.DoWork += (o, args) =>
            {
                // Проходим по всем файлам в папке с исходными файлами
                foreach (string filePath in Directory.GetFiles(targetFolderPath))
                {
                    // Проверяем, содержит ли файл запрещенные слова
                    bool containsForbiddenWords = ContainsForbiddenWords(filePath);

                    // Если файл содержит запрещенные слова, копируем его в папку с целевыми файлами
                    if (containsForbiddenWords)
                    {
                        // Копируем исходный файл
                        CopyFile(filePath, Path.Combine(copyFolderPath, Path.GetFileName(filePath)));
                       
                        // Создаем новый файл с замененными запрещенными словами
                        string newFilePath = Path.Combine(copyFolderPath, Path.GetFileNameWithoutExtension(filePath) + "_censored.txt");
                        ReplaceForbiddenWords(filePath, newFilePath);
                    }
                }

                // Уведомляем основной поток о завершении работы
                worker.ReportProgress(100);
            };

            // Задаем обработчик прогресса
            worker.ProgressChanged += (o, args) =>
            {
                // Обновляем индикатор прогресса
                progressBar.Value = (int)args.ProgressPercentage;
            };

            // Запускаем фоновую задачу
            worker.RunWorkerAsync();

            buttonReport.Enabled = true;
        }
        private bool ContainsForbiddenWords(string filePath)
        {
            // Читаем содержимое файла
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Проходим по всем словам в файле
                foreach (string word in reader.ReadToEnd().Split())
                {
                    // Проверяем, содержится ли слово в списке запрещенных слов
                    foreach (string forbiddenWord in forbiddenWords)
                    {
                        if (word.Equals(forbiddenWord, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void ReplaceForbiddenWords(string filePath, string newFilePath)
        {
            int count = 0;
            // Читаем содержимое файла
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Создаем новый файл
                using (StreamWriter writer = new StreamWriter(newFilePath))
                {
                    // Проходим по всем словам в файле
                    while (!reader.EndOfStream)
                    {
                        string word = reader.ReadLine();

                        // Если слово является запрещенным, заменяем его на 7 звезд
                        if (forbiddenWords.Contains(word))
                        {
                            count++;
                            writer.WriteLine("*******");
                        }
                        else
                        {
                            writer.WriteLine(word);
                        }
                    }
                }

                // Создайте объект для записи в файл
                using (StreamWriter writer = new StreamWriter(textBoxCopyFolder.Text + "\\report.txt", append: true))
                {
                    var f = new FileInfo(filePath);
                    // Запишите в файл количество заменённых слов
                    reportText.Add("Количество заменённых слов в " + filePath + " (" + f.Length + " bytes): " + count);
                    writer.WriteLine("Количество заменённых слов в " + filePath + ": " + count);
                }
            }
        }

        private void CopyFile(string filePath, string newFilePath)
        {
            using (FileStream newFile = new FileStream(newFilePath, FileMode.Create))
            {
                using (FileStream oldFile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = oldFile.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        newFile.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        private void buttonTarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @textBoxTargetFolder.Text;
            folderBrowserDialog.Description = "Выберите папку с исходными файлами";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxTargetFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @textBoxCopyFolder.Text;
            folderBrowserDialog.Description = "Выберите папку для сохранения копий";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxCopyFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {

        }

        private void buttonPause_Click(object sender, EventArgs e)
        {

        }

        private void buttonResume_Click(object sender, EventArgs e)
        {

        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            listBoxReport.Items.Clear();
            foreach (string str in reportText)
            {
                listBoxReport.Items.Add(str);
            }
        }
    }
}
