using Application.Core.Abstractions.Messaging;

namespace MOT.Application.Mo.Commands.Receive;

public sealed class ReceiveMoCommand(
    int moId,
    string telco,
    string serviceNum,
    string phone,
    string syntax,
    string encryptedMessage,
    string signature,
    string moSource) : ICommand<string>
{
    public int MoId { get; } = moId;

    public string Telco { get; } = telco;

    public string ServiceNum { get; } = serviceNum;

    public string Phone { get; } = phone;

    public string Syntax { get; } = syntax;

    public string EncryptedMessage { get; } = encryptedMessage;

    public string Signature { get; } = signature;

    public string MoSource { get; } = moSource;
}