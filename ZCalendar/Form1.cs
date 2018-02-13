using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZDateTime;

namespace ZCalendar
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
			var start_day = new DateTime( DateTime.Now.Year, 1, 1 );
			var today = DateTime.Now;
			var today_color = ( int ) ( 255.0 * double.Parse( Utils.encodeTime( today ).Remove( 3, 1 ) ) / 1e+6 );

			for ( int i = 0,
					end = ( new DateTime( start_day.Year, 12, 31 ).DayOfYear - start_day.DayOfYear );
				i <= end;
				++i )
				Controls.Add( new Label
				{
					BackColor = ( ( i + 1 ) < today.DayOfYear )
						? Color.Black
						: ( ( i + 1 ) == today.DayOfYear )
							? Color.FromArgb( 255 - today_color, 255 - today_color, 255 - today_color )
							: Color.White,
					ForeColor = ( ( i + 1 ) < today.DayOfYear )
						? Color.White
						: ( ( i + 1 ) == today.DayOfYear )
							? Color.FromArgb( today_color, today_color, today_color )
							: Color.Black,
					Font = new Font( "GohuFont", 16 ),
					Text = Utils.encodeDate( start_day.AddDays( i ) ),
					Location = new Point( 12 + ( i % 14 ) * 90, 9 + ( i / 14 ) * 30 ),
					AutoSize = true
				} );

			timer_main.Tick += ( sender, args ) =>
				Text = $@"Today is {Utils.encodeDate( today )} @ {Utils.encodeTime( DateTime.Now )}";
		}
	}
}