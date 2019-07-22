// Generated by https://quicktype.io

namespace CanvasAPIWrapper
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class DiscussionTopic
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("last_reply_at")]
        public DateTimeOffset? LastReplyAt { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("delayed_post_at")]
        public object DelayedPostAt { get; set; }

        [JsonProperty("posted_at")]
        public DateTimeOffset? PostedAt { get; set; }

        [JsonProperty("assignment_id")]
        public object AssignmentId { get; set; }

        [JsonProperty("root_topic_id")]
        public object RootTopicId { get; set; }

        [JsonProperty("position")]
        public object Position { get; set; }

        [JsonProperty("podcast_has_student_posts")]
        public bool? PodcastHasStudentPosts { get; set; }

        [JsonProperty("discussion_type")]
        public string DiscussionType { get; set; }

        [JsonProperty("lock_at")]
        public object LockAt { get; set; }

        [JsonProperty("allow_rating")]
        public bool? AllowRating { get; set; }

        [JsonProperty("only_graders_can_rate")]
        public bool? OnlyGradersCanRate { get; set; }

        [JsonProperty("sort_by_rating")]
        public bool? SortByRating { get; set; }

        [JsonProperty("is_section_specific")]
        public bool? IsSectionSpecific { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("discussion_subentry_count")]
        public long? DiscussionSubentryCount { get; set; }

        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }

        [JsonProperty("require_initial_post")]
        public object RequireInitialPost { get; set; }

        [JsonProperty("user_can_see_posts")]
        public bool? UserCanSeePosts { get; set; }

        [JsonProperty("podcast_url")]
        public object PodcastUrl { get; set; }

        [JsonProperty("read_state")]
        public string ReadState { get; set; }

        [JsonProperty("unread_count")]
        public long? UnreadCount { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("topic_children")]
        public object[] TopicChildren { get; set; }

        [JsonProperty("group_topic_children")]
        public object[] GroupTopicChildren { get; set; }

        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }

        [JsonProperty("published")]
        public bool? Published { get; set; }

        [JsonProperty("can_unpublish")]
        public bool? CanUnpublish { get; set; }

        [JsonProperty("locked")]
        public bool? Locked { get; set; }

        [JsonProperty("can_lock")]
        public bool? CanLock { get; set; }

        [JsonProperty("comments_disabled")]
        public bool? CommentsDisabled { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("html_url")]
        public Uri HtmlUrl { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("pinned")]
        public bool? Pinned { get; set; }

        [JsonProperty("group_category_id")]
        public object GroupCategoryId { get; set; }

        [JsonProperty("can_group")]
        public bool? CanGroup { get; set; }

        [JsonProperty("locked_for_user")]
        public bool? LockedForUser { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("user_count")]
        public long? UserCount { get; set; }

        [JsonProperty("todo_date")]
        public object TodoDate { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("avatar_image_url")]
        public Uri AvatarImageUrl { get; set; }

        [JsonProperty("html_url")]
        public Uri HtmlUrl { get; set; }
    }

    public partial class Permissions
    {
        [JsonProperty("attach")]
        public bool? Attach { get; set; }

        [JsonProperty("update")]
        public bool? Update { get; set; }

        [JsonProperty("reply")]
        public bool? Reply { get; set; }

        [JsonProperty("delete")]
        public bool? Delete { get; set; }
    }
}
