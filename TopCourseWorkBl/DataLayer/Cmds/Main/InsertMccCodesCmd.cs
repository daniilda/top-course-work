using System.Collections.Generic;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.DataLayer.Cmds.Main
{
    public record InsertMccCodesCmd(List<MccCode> MccCodes);
}