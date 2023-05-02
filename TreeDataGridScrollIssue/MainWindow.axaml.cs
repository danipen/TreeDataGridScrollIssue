using Avalonia.Controls;

using System.Collections.Generic;

namespace TreeDataGridScrollIssue
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mSearchResultsTable = new ItemSearchResultsTable();
            mContentPanel.Children.Add(mSearchResultsTable.Table!);

            mTextBox.TextChanged += TextBox_TextChanged;
        }

        void TextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            int count = 0;
            switch (mTextBox.Text.Length)
            {
                case 0: count = 1500; break;
                case 1: count = 1000; break;
                case 2: count = 500; break;
                case 3: count = 200; break;
                case 4: count = 16; break;
                case 5: count = 0; break;
                default: count = 0; break;
            }

            List<string> searchResults = GenerateList(count);
            mSearchResultsTable.Update(searchResults);
        }

        List<string> GenerateList(int count)
        {
            List<string> result = new List<string>(count);

            for (int i = 0; i < count; i++)
            {
                result.Add($"Item {i}");
            }

            return result;
        }

        ItemSearchResultsTable mSearchResultsTable;
    }
}