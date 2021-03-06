﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using TodoScheduler.Base;
using TodoScheduler.PageModel;
using x = Xamarin.Forms;

namespace TodoScheduler.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        #region fields & properties

        IEnumerable<Grouping<MenuGroup, MenuItem>> _menuGroups;
        public IEnumerable<Grouping<MenuGroup, MenuItem>> MenuGroups {
            get { return _menuGroups; }
            set { SetProperty(ref _menuGroups, value); }
        }

        #endregion

        #region constructor

        public MenuViewModel()
        {
            CreateMenuGroups();
        }

        #endregion

        #region commands

        ICommand _tapCommand;
        public ICommand TapCommand {
            get { return _tapCommand ?? new x.Command<MenuItem>(TapCommandExecute); }
            set { SetProperty(ref _tapCommand, value); }
        }

        #endregion

        #region private

        private void TapCommandExecute(MenuItem menuItem)
        {
            if (menuItem == null) return;

            App.ResolveDetailPage(menuItem.ViewModelType);
        }

        public void CreateMenuGroups()
        {
            //Create menu groups
            MenuGroup tagGroup = new MenuGroup() { Id = 0, Icon = "tags.png", Title = "Tag items" };
            MenuGroup todoGroup = new MenuGroup() { Id = 1, Icon = "todos.png", Title = "Todo items" };
            MenuGroup configGroup = new MenuGroup() { Id = 2, Icon = "_info.png", Title = "Information" };
            //Create menu items

            var menuItems = new List<MenuItem>()
            {
                //Tags
                new MenuItem() { Group = tagGroup, Icon = "_tags.png", Title = "Tags", ViewModelType = typeof(TagsViewModel) },
                //Todos
                new MenuItem() { Group = todoGroup, Icon = "today.png", Title = "Today", ViewModelType = typeof(TodayViewModel) },
                new MenuItem() { Group = todoGroup, Icon = "tomorrow.png", Title = "Tomorrow", ViewModelType = typeof(TomorrowViewModel) },
                new MenuItem() { Group = todoGroup, Icon = "schedule.png", Title = "Schedule", ViewModelType = typeof(ScheduleViewModel) },
                //Settings
                new MenuItem() { Group = configGroup, Icon = "_info_.png", Title = "About", ViewModelType = typeof(AboutViewModel) }
            };

            var groupedMenuItems = from menu in menuItems
                                   orderby menu.Group.Id
                                   group menu by menu.Group into grouped
                                   select new Grouping<MenuGroup, MenuItem>(grouped.Key, grouped);

            MenuGroups = new ObservableCollection<Grouping<MenuGroup, MenuItem>>(groupedMenuItems);
        }

        #endregion
    }
}
