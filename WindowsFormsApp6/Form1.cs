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

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private List<string> forbiddenWords = new List<string>();
        public Form1()
        {
            InitializeComponent();
            buttonLoad.Enabled = true;
            buttonStart.Enabled = false;
            textBoxTargetFolder.Text = "C:\\Users\\" + Environment.UserName + "\\Desktop";
            textBoxCopyFolder.Text = "C:\\Users\\" + Environment.UserName + "\\Desktop\\test";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private List<string> GetForbiddenWords()
        {
            return forbiddenWords;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            listBoxForbiddenWords.Items.Clear();
            forbiddenWords = new List<string>();
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
            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 1;

            // Получаем пути к папкам с исходными и целевыми файлами
            string targetFolderPath = textBoxTargetFolder.Text;
            string copyFolderPath = textBoxCopyFolder.Text;
            progressBar.Value = 5;

            // Создаем объект BackgroundWorker
            BackgroundWorker worker = new BackgroundWorker();

            // Устанавливаем свойство WorkerReportsProgress в true
            worker.WorkerReportsProgress = true;
            progressBar.Value = 15;

            progressBar.Value = 25;
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

                        // Обновляем индикатор прогресса
                        int progress = 50;
                        progressBar.Value = progress;
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
        }
        private bool ContainsForbiddenWords(string filePath)
        {
            // Читаем содержимое файла
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Проходим по всем словам в файле
                while (!reader.EndOfStream)
                {
                    string word = reader.ReadLine();

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
                            writer.WriteLine("*******");
                        }
                        else
                        {
                            writer.WriteLine(word);
                        }
                    }
                }
            }
        }

        private void CopyFile(string filePath, string newFilePath)
        {
            // Создаем новый файл
            using (FileStream newFile = new FileStream(newFilePath, FileMode.Create))
            {
                // Копируем содержимое исходного файла в новый файл
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
            // Открываем диалоговое окно выбора папки
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @textBoxTargetFolder.Text;
            folderBrowserDialog.Description = "Выберите папку с исходными файлами";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Задаем путь к папке в текстовом поле
                textBoxTargetFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            // Открываем диалоговое окно выбора папки
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @textBoxCopyFolder.Text;
            folderBrowserDialog.Description = "Выберите папку для сохранения копий";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Задаем путь к папке в текстовом поле
                textBoxCopyFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
