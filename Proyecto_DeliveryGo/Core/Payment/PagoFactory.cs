namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoFactory
    {
        public static IPago Create(string tipo)
        {
            return tipo.ToLower() switch
            {
                "tarjeta" => new PagoTarjeta(),
                "transf" => new PagoTransfer(),
                "mp" => new PagoMp()
            };
        }
    }
}