using System.Collections.Generic;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.DataLayer.Cmds.Main
{
    public record InsertTypesCmd(List<Type> Types);
}