using WindscribeNet.Enums;

namespace WindscribeNet.Commands.Models
{
    public class LoginStateInfo
    {
        public LoginStateType State { get; }
        public LoginErrorCode? ErrorCode { get; }
        public string? ErrorMessage { get; }

        public LoginStateInfo(LoginStateType state, LoginErrorCode? errorCode = null, string? errorMessage = null)
        {
            State = state;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return State switch
            {
                LoginStateType.Error => $"Error: {ErrorMessage}",
                _ => EnumConverter.ToString(State)
            };
        }
    }
}
