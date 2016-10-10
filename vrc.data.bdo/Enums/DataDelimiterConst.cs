using vrc.data.bdo.Model;

namespace vrc.data.bdo.Enums
{
    public class DataDelimiterConst
    {
        public static DataDelimiterBdo None => new DataDelimiterBdo
        {
            DataDelimiterId = 1,
            Description = "None",
            Character = null
        };

        public static DataDelimiterBdo Comma => new DataDelimiterBdo
        {
            DataDelimiterId = 2,
            Description = "Comma",
            Character = ","
        };

        public static DataDelimiterBdo Semicolon => new DataDelimiterBdo
        {
            DataDelimiterId = 3,
            Description = "Semi-colon",
            Character = ";"
        };

        public static DataDelimiterBdo Colon => new DataDelimiterBdo
        {
            DataDelimiterId = 4,
            Description = "Colon",
            Character = ":"
        };

        public static DataDelimiterBdo Pipe => new DataDelimiterBdo
        {
            DataDelimiterId = 5,
            Description = "Pipe",
            Character = "|"
        };

        public static DataDelimiterBdo Asterisk => new DataDelimiterBdo
        {
            DataDelimiterId = 6,
            Description = "Asterisk",
            Character = "*"
        };

        public static DataDelimiterBdo Tab => new DataDelimiterBdo
        {
            DataDelimiterId = 7,
            Description = "Tab",
            Character = "	"
        };
    }
}
