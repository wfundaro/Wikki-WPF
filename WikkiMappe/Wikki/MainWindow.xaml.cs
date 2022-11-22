using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wikki.UCs;

namespace Wikki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            // On force l'ouverture du menu Flyout de gauche au démarrage
            //MenuFlyout.IsOpen = true;
        }

        #region Methodes
        private void MoveMenuCursor(int menuItemIndex)
        {
            double itemHeight = ListViewItemHome.Height;
            BorderActiveMenu.Margin = new Thickness(0, 10 + (menuItemIndex * itemHeight), 0, 0);
        }

        private void ToggleFlyoutOpenCloseFlyout(int flyoutIndex)
        {
            try
            {
                if (this.Flyouts.Items[flyoutIndex] is Flyout flyout)
                {
                    flyout.IsOpen = !flyout.IsOpen;
                }
            }
            catch
            {
                // index lfyout hors limite
            }
        } 
        #endregion

        #region Window buttons Event
        private void WindowDragMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
            catch
            {
                // On evite les plantages lors des moves rares mais au cas où
            }
        }

        private void BtnWindowMinimize_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void BtnWindowMaximize_click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton tb)
            {
                if (tb.IsChecked == true)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            }
        }

        private async void BtnWindowClose_click(object sender, RoutedEventArgs e)
        {
            // Until developement remove in finale app
            Application.Current.Shutdown();

            MetroDialogSettings mds = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OUI",
                NegativeButtonText = "NON",
                AnimateShow = true,
                AnimateHide = true
            };

             MessageDialogResult result = await this.ShowMessageAsync("Attention",
                "Voulez-vous quitter l'application ?",
                MessageDialogStyle.AffirmativeAndNegative, mds);

            if(result == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region Event widget
        private void BtnHamburgerMenu_Click(object sender, RoutedEventArgs e)
        {
            ToggleFlyoutOpenCloseFlyout(0);
        }

        private void MainMenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int itemIndex = MainMenuList.SelectedIndex;
            MoveMenuCursor(itemIndex);
            BtnHamburgerMenu.IsChecked = false;
            ToggleFlyoutOpenCloseFlyout(0);
            
            switch (itemIndex)
            {
                case 0:
                    UCPlaceHolderGrid.Children.Clear();
                    UCPlaceHolderGrid.Children.Add(new UCHome());
                    break;
                case 1:
                    UCPlaceHolderGrid.Children.Clear();
                    UCPlaceHolderGrid.Children.Add(new UCAddPerson());
                    break;
                case 2:
                    UCPlaceHolderGrid.Children.Clear();
                    UCPlaceHolderGrid.Children.Add(new UCStatistique());
                    break;
                default:
                    break;
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UCPlaceHolderGrid.Children.Clear();
            UCPlaceHolderGrid.Children.Add(new UCHome());
        }
        #endregion

    }
}
