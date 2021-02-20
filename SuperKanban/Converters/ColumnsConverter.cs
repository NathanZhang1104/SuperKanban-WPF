using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using SuperKanban.Model.Entities;
using Syncfusion.UI.Xaml.Kanban;

namespace SuperKanban.Converters
{
    public class ColumnsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            KanbanColumnsCollection ret=new KanbanColumnsCollection();
            foreach (var colitem in value as ObservableCollection<BoardColumn>)
            {
                var cur_col = new KanbanColumn() { Title = colitem.Title, Categories = colitem.Category };
                ret.Add(cur_col);
            }
            return ret;
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<BoardColumn> BoardColumns = new ObservableCollection<BoardColumn>();


            foreach (var item in (value as KanbanColumnsCollection))
            {
                BoardColumns.Add(new BoardColumn(item));
            }
              return BoardColumns;
            throw new NotImplementedException();
        }
    }
}
