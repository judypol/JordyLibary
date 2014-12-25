using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class ContextMenuSettings:BindingSettingsBase
    {
        public ContextMenuSettings()
            :base("kendoContextMenu")
        { }
        /// <summary>
        /// Specifies that ContextMenu should be shown aligned to the target or the filter element if specified.
        /// </summary>
        public bool? AlignToAnchor { get; set; }
        public bool? CloseOnClick { get; set; }
        /// <summary>
        /// Specifies ContextMenu's sub menu opening direction. Can be "top", "bottom", "left", "right". 
        /// default is default
        /// </summary>
        public string Direction { get; set; }
        public string Filter { get; set; }
        /// <summary>
        /// Specifies the delay in ms before the sub menus 
        /// are opened/closed - used to avoid accidental closure on leaving.
        /// default is 100ms
        /// </summary>
        public int? HoverDelay { get; set; }
        /// <summary>
        /// Root menu orientation. Could be horizontal or vertical.
        /// default is vertical
        /// </summary>
        public string Orientation { get; set; }
        /// <summary>
        /// Specifies how ContextMenu should adjust to screen boundaries. 
        /// By default the strategy is "fit" for a sub menu with a horizontal 
        /// parent or the root menu, meaning it will move to fit in screen 
        /// boundaries in all directions, and "fit flip" for a sub menu with vertical parent, 
        /// meaning it will fit vertically and flip over its parent horizontally. 
        /// You can also switch off the screen boundary detection completely if you set the popupCollision to false.
        /// </summary>
        public string PopupCollision { get; set; }
        /// <summary>
        /// Specifies the event or events on which ContextMenu should open. By default ContextMenu will 
        /// show on contextmenu event on desktop and hold event on touch devices. Could be 
        /// any pointer/mouse/touch event, also several, separated by spaces.
        /// </summary>
        public string ShowOn { get; set; }
        /// <summary>
        /// Specifies the element on which ContextMenu should open. The default element is the document body.
        /// </summary>
        public string Target { get; set; }
    }
}
