// Generated by https://quicktype.io

namespace CanvasWrapperRefit
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Course
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("start_at")]
        public object StartAt { get; set; }

        [JsonProperty("grading_standard_id")]
        public long GradingStandardId { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("course_code")]
        public string CourseCode { get; set; }

        [JsonProperty("default_view")]
        public string DefaultView { get; set; }

        [JsonProperty("root_account_id")]
        public long RootAccountId { get; set; }

        [JsonProperty("enrollment_term_id")]
        public long EnrollmentTermId { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("end_at")]
        public object EndAt { get; set; }

        [JsonProperty("public_syllabus")]
        public bool PublicSyllabus { get; set; }

        [JsonProperty("public_syllabus_to_auth")]
        public bool PublicSyllabusToAuth { get; set; }

        [JsonProperty("storage_quota_mb")]
        public long StorageQuotaMb { get; set; }

        [JsonProperty("is_public_to_auth_users")]
        public bool IsPublicToAuthUsers { get; set; }

        [JsonProperty("hide_final_grades")]
        public bool HideFinalGrades { get; set; }

        [JsonProperty("apply_assignment_group_weights")]
        public bool ApplyAssignmentGroupWeights { get; set; }

        [JsonProperty("calendar")]
        public Calendar Calendar { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("blueprint")]
        public bool Blueprint { get; set; }

        [JsonProperty("blueprint_restrictions")]
        public BlueprintRestrictions BlueprintRestrictions { get; set; }

        [JsonProperty("sis_course_id")]
        public object SisCourseId { get; set; }

        [JsonProperty("integration_id")]
        public object IntegrationId { get; set; }

        [JsonProperty("enrollments")]
        public object[] Enrollments { get; set; }

        [JsonProperty("workflow_state")]
        public string WorkflowState { get; set; }

        [JsonProperty("restrict_enrollments_to_course_dates")]
        public bool RestrictEnrollmentsToCourseDates { get; set; }

        [JsonProperty("overridden_course_visibility")]
        public string OverriddenCourseVisibility { get; set; }
    }

    public class BlueprintRestrictions
    {
        [JsonProperty("content")]
        public bool Content { get; set; }

        [JsonProperty("points")]
        public bool Points { get; set; }

        [JsonProperty("due_dates")]
        public bool DueDates { get; set; }

        [JsonProperty("availability_dates")]
        public bool AvailabilityDates { get; set; }
    }

    public class Calendar
    {
        [JsonProperty("ics")]
        public Uri Ics { get; set; }
    }
}
