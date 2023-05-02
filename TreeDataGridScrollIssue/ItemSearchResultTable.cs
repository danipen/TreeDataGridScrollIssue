using System.Collections.Generic;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Selection;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;

namespace TreeDataGridScrollIssue
{
    internal class ItemSearchResultsTable
    {
        internal TreeDataGrid? Table { get { return mTable; } }

        internal ItemSearchResultsTable()
        {
            mTable = new TreeDataGrid();
            mTable.ShowColumnHeaders = false;

            mSource = BuildTableSource();
            ((ITreeSelectionModel)mSource.Selection).SingleSelect = true;

            mTable.BeginInit();
            mTable.Source = mSource;
            mTable.EndInit();
        }

        internal void Update(List<string> results)
        {
            mSource!.Items = results;
        }

        FlatTreeDataGridSource<string> BuildTableSource()
        {
            FlatTreeDataGridSource<string> result =
                new FlatTreeDataGridSource<string>(new List<string>());

            result.Columns.Add(BuildTemplateColumn());

            return result;
        }

        IColumn<string> BuildTemplateColumn()
        {
            return new TemplateColumn<string>(
                "",
                new FuncDataTemplate<string>(BuildCellControl, false),
                new GridLength(1.0, GridUnitType.Star));
        }

        Control? BuildCellControl(string searchResult, INameScope ns)
        {
            if (searchResult == null)
                return null;

            return CreatePathSearchResultPanel(searchResult);
        }

        static Panel CreatePathSearchResultPanel(
            string searchResult)
        {
            StackPanel namePanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
            };

            namePanel.Margin = new Thickness(0, 0, 0, 3);

            TextBlock pathBlock =
                new TextBlock();
            pathBlock.Text = searchResult;

            Panel result = new Panel()
            {
                Background = Brushes.Transparent,
                Children = { pathBlock }
            };

            return result;
        }

        TreeDataGrid? mTable;
        FlatTreeDataGridSource<string>? mSource;
    }
}
