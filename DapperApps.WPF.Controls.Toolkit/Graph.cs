using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DapperApps.WPF.Controls
{
    [TemplatePart(Name = LayoutRootName, Type = typeof(Grid))]
    [TemplatePart(Name = ScrollViewerName, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = CanvasName, Type = typeof(Canvas))]
    public class Graph : Control
    {
        private const string LayoutRootName = "LayoutRoot";
        private const string ScrollViewerName = "GraphScroll";
        private const string CanvasName = "GraphRoot";

        private Grid _layoutRoot;
        private ScrollViewer _graphScroll;
        private Canvas _graphRoot;

        public Graph() : base()
        {
            this.DefaultStyleKey = typeof(Graph);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
