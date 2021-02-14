using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using SuperKanban.Model.Entities;
using SuperKanban.ViewModel;
namespace SuperKanban.Converters
{
    public class CardToViewModelConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Card card = value as Card;
            CardViewModel cardViewModel = new CardViewModel();
            cardViewModel.Card = card;
            return cardViewModel;
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
