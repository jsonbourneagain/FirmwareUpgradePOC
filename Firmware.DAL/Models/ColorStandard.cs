
using System.ComponentModel.DataAnnotations;

namespace Firmware.DAL.Models
{
    public enum ColorStandard
    {
        [Display(Name = "NTSC")]
        NTSC = 1,
        [Display(Name = "PAL")]
        PAL = 2,
        [Display(Name = "NTSC + PAL")]
        NTSCANDPAL = 3
    }
}
