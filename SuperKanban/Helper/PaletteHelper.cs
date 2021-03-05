using MaterialDesignColors;
using System;
using System.Windows;
using MaterialDesignColors.ColorManipulation;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{

    public class SPKBTheme:ITheme
    {
        ColorPair ITheme.PrimaryLight { get; set; }
        Color ITheme.TextAreaBorder { get; set; }
        Color ITheme.TextFieldBoxDisabledBackground { get; set; }
        Color ITheme.TextFieldBoxHoverBackground { get; set; }
        Color ITheme.TextFieldBoxBackground { get; set; }
        Color ITheme.TextBoxBorder { get; set; }
        Color ITheme.SnackbarRipple { get; set; }
        Color ITheme.SnackbarMouseOver { get; set; }
        Color ITheme.SnackbarBackground { get; set; }
        Color ITheme.ChipBackground { get; set; }
        Color ITheme.ToolTipBackground { get; set; }
        Color ITheme.FlatButtonRipple { get; set; }
        Color ITheme.FlatButtonClick { get; set; }
        Color ITheme.ToolBackground { get; set; }
        Color ITheme.ToolForeground { get; set; }
        Color ITheme.Selection { get; set; }
        Color ITheme.Divider { get; set; }
        Color ITheme.CheckBoxDisabled { get; set; }
        ColorPair ITheme.PrimaryMid { get; set; }
        ColorPair ITheme.PrimaryDark { get; set; }
        ColorPair ITheme.SecondaryLight { get; set; }
        ColorPair ITheme.SecondaryMid { get; set; }
        ColorPair ITheme.SecondaryDark { get; set; }
        Color ITheme.ValidationError { get; set; }
        Color ITheme.TextAreaInactiveBorder { get; set; }
        Color ITheme.Background { get; set; }
        Color ITheme.CardBackground { get; set; }
        Color ITheme.ToolBarBackground { get; set; }
        Color ITheme.Body { get; set; }
        Color ITheme.BodyLight { get; set; }
        Color ITheme.ColumnHeader { get; set; }
        Color ITheme.CheckBoxOff { get; set; }
        Color ITheme.Paper { get; set; }
        Color ITheme.DataGridRowHoverBackground { get; set; }
    }
    public static class PaletteHelperExtensions
    {
        public static void ChangePrimaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten());
            theme.SecondaryMid = new ColorPair(color);
            theme.SecondaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
    }

}
