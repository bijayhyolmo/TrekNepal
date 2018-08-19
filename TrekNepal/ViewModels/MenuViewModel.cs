using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrekNepal.ViewModels
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }

        public int ParentId { get; set; }

        public string DisplayText { get; set; }

        public string TargetUrl { get; set; }

        public List<MenuViewModel> SubMenus { get; set; }

        public List<MenuViewModel> GetMenus()
        {
            return new List<MenuViewModel> {
                new MenuViewModel { MenuId =1, ParentId = 0, DisplayText = "Home", TargetUrl = "/home"},
                new MenuViewModel {
                    MenuId = 2,
                    ParentId = 0,
                    DisplayText = "Nepal",
                    TargetUrl = "#",
                    SubMenus = new List<MenuViewModel>
                    {
                        new MenuViewModel {
                            MenuId =6,
                            ParentId = 2,
                            DisplayText = "Trekking in Nepal",
                            TargetUrl = "#",
                            SubMenus = new List<MenuViewModel> {
                                new MenuViewModel {
                                    MenuId =6,
                                    ParentId = 2,
                                    DisplayText = "Everest Region",
                                    TargetUrl = "#",
                                    SubMenus = new List<MenuViewModel> {
                                        new MenuViewModel {
                                            MenuId =6,
                                            ParentId = 2,
                                            DisplayText = "Everest Regison",
                                            TargetUrl = "#",
                                        },
                                    },
                                }
                            },
                        },
                        new MenuViewModel {
                            MenuId =7,
                            ParentId = 2,
                            DisplayText = "Day Tours",
                            TargetUrl = "#",
                            SubMenus = new List<MenuViewModel> {
                                new MenuViewModel { MenuId =6, ParentId = 2, DisplayText = "Private Tour To Nagarkot Sunrise",  TargetUrl = "#"},
                            },
                        },
                    },
                },
                new MenuViewModel { MenuId =3, ParentId = 0, DisplayText = "Gallery", TargetUrl = "/public/gallery"},
                new MenuViewModel { MenuId =4, ParentId = 0, DisplayText = "About Us", TargetUrl = "/public/aboutus"},
                new MenuViewModel { MenuId =5, ParentId = 0, DisplayText = "Contact Us", TargetUrl = "/public/contactus"},
            };
        }
    }
}