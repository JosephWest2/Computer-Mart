using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Computer_Mart.Models
{
	public class Computer : ComponentBase
	{
		[DisplayName("CPU")]
		public int? CPUId { get; set; }
		public CPU? CPU { get; set; }
		[DisplayName("GPU")]
		public int? GPUId { get; set; }
		public GPU? GPU { get; set; }
		[DisplayName("RAM")]
		public int? RAMId { get; set; }
		public RAM? RAM { get; set; }
		[DisplayName("SSD")]
		public int? SSDId { get; set; }
		public SSD? SSD { get; set; }
	}
}
