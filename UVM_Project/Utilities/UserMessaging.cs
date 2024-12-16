using CommunityToolkit.Mvvm.Messaging.Messages;

namespace UVM_Project.Utilities
{
    public class UserMessaging : ValueChangedMessage<UserMessage>
    {
        public UserMessaging(UserMessage value) : base(value)
        {

        }
    }
}
