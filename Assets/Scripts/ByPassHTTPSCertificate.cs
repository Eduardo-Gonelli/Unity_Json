using UnityEngine.Networking;

// Ignora a validação do certificado de segurança HTTPS (para http apenas se o servidor não suportar https)
public class ByPassHTTPSCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
