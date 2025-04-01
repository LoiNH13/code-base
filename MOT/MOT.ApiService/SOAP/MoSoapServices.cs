// ReSharper disable InconsistentNaming

using MediatR;
using MOT.Application.Mo.Commands.Receive;

namespace MOT.ApiService.SOAP;

/// <summary>
///     This class provides methods for handling SOAP requests related to Mobile Originated (MO) messages.
/// </summary>
public sealed class MoSoapServices : IMoSoapServices
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MoSoapServices" /> class.
    /// </summary>
    /// <param name="mediator">The mediator for handling requests.</param>
    public MoSoapServices(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Processes a MO message request.
    /// </summary>
    /// <param name="MOId">The unique identifier of the MO message.</param>
    /// <param name="Telco">The telecommunications operator.</param>
    /// <param name="ServiceNum">The service number.</param>
    /// <param name="Phone">The phone number of the sender.</param>
    /// <param name="Syntax">The syntax of the message.</param>
    /// <param name="EncryptedMessage">The encrypted message content.</param>
    /// <param name="Signature">The signature of the message.</param>
    /// <returns>
    ///     A string representing the response to the MO message request.
    ///     The format is "00|Message response" for successful processing, or "01|Param Invalid" if any parameter is invalid.
    /// </returns>
    public string ReceiveMessage(int MOId, string Telco, string ServiceNum, string Phone, string Syntax,
        string EncryptedMessage,
        string Signature)
    {
        if (MOId <= 0 || string.IsNullOrEmpty(Telco) || string.IsNullOrEmpty(ServiceNum) ||
            string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Syntax) || string.IsNullOrEmpty(EncryptedMessage) ||
            string.IsNullOrEmpty(Signature))
            // check param input empty return text
            return "01|Param Invalid";

        return _mediator.Send(
                new ReceiveMoCommand(MOId, Telco, ServiceNum, Phone, Syntax, EncryptedMessage, Signature, "FTI"))
            .Result;
    }
}