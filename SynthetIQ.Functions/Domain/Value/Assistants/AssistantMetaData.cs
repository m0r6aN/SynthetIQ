namespace SynthetIQ.Functions.Domain.Value.Assistants
{
    public sealed class AssistantMetaData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public List<string> Capabilities { get; set; }
    }

    public enum AssistantCapability
    {
        CreativeWriting = 0, // Generates original content, stories, marketing copy, etc.
        Math = 1,            // Solves mathematical problems, provides explanations, and visualizes concepts.
        Coding = 2,          // Writes, analyzes, and debugs code in various programming languages.
        Images = 3,          // Generates, edits, and analyzes images and visual content.
        Speech = 4,          // Transcribes speech to text, generates voice outputs, and understands spoken commands.
        ProjectManagement = 5,  // Helps with planning, tracking, and managing projects.
        Moderation = 6,       // Filters and moderates content to ensure it meets certain guidelines or standards.
        Documentation = 7,    // Creates, edits, and manages all aspects of documentation and technical writing
        Translation = 8,      // Extremely useful for global applications requiring multi-language support.
        DataAnalysis = 9,     // Assistants with capabilities in data analysis can help interpret data, generate reports, or even predict trends.
        Education = 10,       // This capability could support learning platforms by providing tutoring, creating educational content, or automating quiz generation.
        HealthAndFitness = 11,// For applications focused on wellness, assistants could generate personalized workout plans or nutritional advice
        MusicAndAudio = 12,   // This could involve generating music, editing audio files, or even providing insights on music theory.
        VideoEditing = 13,    // Assistants could suggest edits, compile video content, or automate aspects of the video production process.
        SocialMediaManagement = 14, // This could include content creation suggestions, scheduling posts, or analyzing engagement data.
        SEO = 15,             // Assistants with SEO capabilities could offer site optimization recommendations or keyword analysis.
        CustomerSupport = 16, // AI could automate response drafting for common inquiries, manage ticket queues, or provide live chat services.
        MarketAnalysis = 17,  // Useful for businesses, these assistants could analyze market trends, competitor data, or customer feedback.
        GameDevelopment = 18, // Assistants could help with coding, asset creation, or even logic and level design in game development projects.
        PersonalAssistant = 19// For day-to-day productivity, these assistants could manage calendars, set reminders, or even draft emails.
    }

}
