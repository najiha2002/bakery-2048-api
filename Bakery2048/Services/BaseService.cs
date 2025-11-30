using System.Text.Json;

namespace Bakery2048.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected List<T> items;
        protected readonly string dataFilePath;

        protected BaseService(List<T> itemList, string filePath)
        {
            items = itemList;
            dataFilePath = filePath;
            LoadFromFile();
        }

        protected void SaveToFile()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string jsonString = JsonSerializer.Serialize(items, options);
                File.WriteAllText(dataFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        protected void LoadFromFile()
        {
            try
            {
                if (File.Exists(dataFilePath))
                {
                    string jsonString = File.ReadAllText(dataFilePath);
                    
                    // Debug: Check if file is empty
                    if (string.IsNullOrWhiteSpace(jsonString) || jsonString.Trim() == "[]")
                    {
                        Console.WriteLine($"⚠ Warning: {dataFilePath} is empty or has no data.");
                        return;
                    }
                    
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    
                    var loadedItems = JsonSerializer.Deserialize<List<T>>(jsonString, options);

                    if (loadedItems != null && loadedItems.Count > 0)
                    {
                        items.Clear();
                        items.AddRange(loadedItems);
                        Console.WriteLine($"✓ Loaded {items.Count} {typeof(T).Name.ToLower()}(s) from {dataFilePath}.");
                    }
                    else
                    {
                        Console.WriteLine($"⚠ Deserialization returned null or empty for {dataFilePath}");
                    }
                }
                else
                {
                    Console.WriteLine($"ℹ File not found: {dataFilePath} - starting with empty list.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading data from {dataFilePath}: {ex.Message}");
                Console.WriteLine($"   Details: {ex.InnerException?.Message}");
            }
        }

        protected void PauseForUser()
        {
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }

        public abstract void ShowMenu();
    }
}
