using System.Windows;
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
                x => x.Value,
                x => x.Text,
                x => new Domain.Sample.SampleTextVO(x));

            InitializeComponent();
        }
    }
}
