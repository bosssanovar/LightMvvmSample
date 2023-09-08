using System.Windows;
using Domain.Sample;
using Reactive.Bindings.Extensions;

namespace WpfApp1
{
    /// <summary>
    /// SampleのView責務コード記述部
    /// </summary>
    public partial class Sample : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Sample()
        {
            // Init VM
            _model = new();

            Text = _model.Text.ToReactivePropertySlimAsSynchronized(
                x => x.Value,                          // Selector
                x => x.Text,                           // Convert
                x => new SampleTextVO(x) // ConvertBack
                );

            InitializeComponent();
        }
    }
}
