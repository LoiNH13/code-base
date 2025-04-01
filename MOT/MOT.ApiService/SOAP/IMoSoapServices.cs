using System.ServiceModel;

// ReSharper disable All

namespace MOT.ApiService.SOAP;

/// <summary>
///     soap service
/// </summary>
[ServiceContract]
public interface IMoSoapServices
{
    /// <summary>
    ///     receive message
    /// </summary>
    /// <param name="MOId">Message Originator ID</param>
    /// <param name="Telco">Telecommunication company</param>
    /// <param name="ServiceNum">Service number</param>
    /// <param name="Phone">Phone number</param>
    /// <param name="Syntax">Message syntax</param>
    /// <param name="EncryptedMessage">Encrypted message content</param>
    /// <param name="Signature">Message signature</param>
    /// <returns>Response message</returns>
    [OperationContract]
    string ReceiveMessage(int MOId, string Telco, string ServiceNum, string Phone, string Syntax,
        string EncryptedMessage, string Signature);
}