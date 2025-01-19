using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using incity_test.DataAccess;
using incity_test.Infrastructure;
using incity_test.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace incity_test;

public class IncityViewModel
{
    public ObservableCollection<DocumentDb> Items { get; set; } = new ObservableCollection<DocumentDb>();

    public ICommand LoadJsonCommand { get; }

    public IncityViewModel()
    {
        using (var context = new IncityDbContext())
        {
            context.Database.EnsureCreated();
        }

        LoadJsonCommand = new AsyncCommand(LoadJson);
    }

    private async Task LoadJson()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Загрузи Json файл",
            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            string filePath = dialog.FileName;

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                var parsedData = jsonContent.Parse();

                var documentDb = parsedData.Adapt<DocumentDb>();

                List<DocumentDb> documents;
                using (var context = new IncityDbContext())
                {
                    await context.Documents.AddAsync(documentDb).ConfigureAwait(false);
                    await context.SaveChangesAsync().ConfigureAwait(false);

                    documents = await context.Documents.ToListAsync().ConfigureAwait(false);
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    Items.Clear();
                    foreach (var document in documents)
                    {
                        Items.Add(document);
                    }
                });

                MessageBox.Show("Загружено", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}