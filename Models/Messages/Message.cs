namespace TourAgency.Models.Messages {
    public class Message {
        public string type;
        public string message;

        public Message(string type, string message) {
            this.type = type;
            this.message = message;
        }
    }
}
