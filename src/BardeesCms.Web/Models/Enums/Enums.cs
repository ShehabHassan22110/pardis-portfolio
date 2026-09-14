namespace BardeesCms.Web.Models.Enums;

/// <summary>Type of a media item within a portfolio project or the media library.</summary>
public enum MediaType
{
    Image = 0,
    Video = 1,
    Document = 2,
    Logo = 3
}

/// <summary>How a video should be rendered on the frontend.</summary>
public enum VideoProvider
{
    YouTube = 0,
    Vimeo = 1,
    Mp4 = 2,
    External = 3
}

/// <summary>Lifecycle status of an inbound contact/booking message.</summary>
public enum MessageStatus
{
    Unread = 0,
    Read = 1,
    Replied = 2,
    Archived = 3
}

/// <summary>Where a navigation item is rendered.</summary>
public enum NavLocation
{
    Header = 0,
    Footer = 1
}

/// <summary>Publish status of a discipline / category.</summary>
public enum DisciplineStatus
{
    Live = 0,
    Soon = 1
}

/// <summary>How an abaya design is offered.</summary>
public enum AbayaType
{
    /// <summary>Available to buy as shown.</summary>
    ReadyToWear = 0,
    /// <summary>Made-to-measure / tailored on request.</summary>
    Custom = 1
}
