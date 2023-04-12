namespace EmployeeApi.Infrastructure.Models
{
    public enum Positions
    {
        Ceo,
        Boss,
        ProductManager,
        InformationAnalist,
        QA,
        SoftwareDeveloper,
        HR,
        NotDefinedYet
    }

    public class PositionsHelper
    {
        public static Positions MapStringToPosition(string position)
        {
            return position switch
            {
                "Ceo" => Positions.Ceo,
                "Boss" => Positions.Boss,
                "Product Manager" => Positions.ProductManager,
                "Information Analist" => Positions.InformationAnalist,
                "QA" => Positions.QA,
                "Software developer" or "Developer" => Positions.SoftwareDeveloper,
                "HR" => Positions.HR,
                _ => Positions.NotDefinedYet,
            };
        }
    }
}