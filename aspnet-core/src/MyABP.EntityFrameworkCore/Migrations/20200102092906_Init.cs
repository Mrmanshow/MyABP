using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyABP.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abp_audit_logs",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: true),
                    service_name = table.Column<string>(maxLength: 256, nullable: true),
                    method_name = table.Column<string>(maxLength: 256, nullable: true),
                    parameters = table.Column<string>(maxLength: 1024, nullable: true),
                    return_value = table.Column<string>(nullable: true),
                    execution_time = table.Column<DateTime>(nullable: false),
                    execution_duration = table.Column<int>(nullable: false),
                    client_ip_address = table.Column<string>(maxLength: 64, nullable: true),
                    client_name = table.Column<string>(maxLength: 128, nullable: true),
                    browser_info = table.Column<string>(maxLength: 512, nullable: true),
                    exception = table.Column<string>(maxLength: 2000, nullable: true),
                    impersonator_user_id = table.Column<long>(nullable: true),
                    impersonator_tenant_id = table.Column<int>(nullable: true),
                    custom_data = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_audit_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_background_jobs",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    job_type = table.Column<string>(maxLength: 512, nullable: false),
                    job_args = table.Column<string>(maxLength: 1048576, nullable: false),
                    try_count = table.Column<short>(nullable: false),
                    next_try_time = table.Column<DateTime>(nullable: false),
                    last_try_time = table.Column<DateTime>(nullable: true),
                    is_abandoned = table.Column<bool>(nullable: false),
                    priority = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_background_jobs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_editions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    name = table.Column<string>(maxLength: 32, nullable: false),
                    display_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_editions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_entity_change_sets",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    browser_info = table.Column<string>(maxLength: 512, nullable: true),
                    client_ip_address = table.Column<string>(maxLength: 64, nullable: true),
                    client_name = table.Column<string>(maxLength: 128, nullable: true),
                    creation_time = table.Column<DateTime>(nullable: false),
                    extension_data = table.Column<string>(nullable: true),
                    impersonator_tenant_id = table.Column<int>(nullable: true),
                    impersonator_user_id = table.Column<long>(nullable: true),
                    reason = table.Column<string>(maxLength: 256, nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_entity_change_sets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_language_texts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    language_name = table.Column<string>(maxLength: 128, nullable: false),
                    source = table.Column<string>(maxLength: 128, nullable: false),
                    key = table.Column<string>(maxLength: 256, nullable: false),
                    value = table.Column<string>(maxLength: 67108864, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_language_texts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_languages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    display_name = table.Column<string>(maxLength: 64, nullable: false),
                    icon = table.Column<string>(maxLength: 128, nullable: true),
                    is_disabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_notification_subscriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    notification_name = table.Column<string>(maxLength: 96, nullable: true),
                    entity_type_name = table.Column<string>(maxLength: 250, nullable: true),
                    entity_type_assembly_qualified_name = table.Column<string>(maxLength: 512, nullable: true),
                    entity_id = table.Column<string>(maxLength: 96, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_notification_subscriptions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    notification_name = table.Column<string>(maxLength: 96, nullable: false),
                    data = table.Column<string>(maxLength: 1048576, nullable: true),
                    data_type_name = table.Column<string>(maxLength: 512, nullable: true),
                    entity_type_name = table.Column<string>(maxLength: 250, nullable: true),
                    entity_type_assembly_qualified_name = table.Column<string>(maxLength: 512, nullable: true),
                    entity_id = table.Column<string>(maxLength: 96, nullable: true),
                    severity = table.Column<byte>(nullable: false),
                    user_ids = table.Column<string>(maxLength: 131072, nullable: true),
                    excluded_user_ids = table.Column<string>(maxLength: 131072, nullable: true),
                    tenant_ids = table.Column<string>(maxLength: 131072, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_organization_unit_roles",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    role_id = table.Column<int>(nullable: false),
                    organization_unit_id = table.Column<long>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_organization_unit_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_organization_units",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    parent_id = table.Column<long>(nullable: true),
                    code = table.Column<string>(maxLength: 95, nullable: false),
                    display_name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_organization_units", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_organization_units_abp_organization_units_parent_id",
                        column: x => x.parent_id,
                        principalTable: "abp_organization_units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_tenant_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    notification_name = table.Column<string>(maxLength: 96, nullable: false),
                    data = table.Column<string>(maxLength: 1048576, nullable: true),
                    data_type_name = table.Column<string>(maxLength: 512, nullable: true),
                    entity_type_name = table.Column<string>(maxLength: 250, nullable: true),
                    entity_type_assembly_qualified_name = table.Column<string>(maxLength: 512, nullable: true),
                    entity_id = table.Column<string>(maxLength: 96, nullable: true),
                    severity = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_tenant_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_accounts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    user_link_id = table.Column<long>(nullable: true),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    email_address = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_login_attempts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<int>(nullable: true),
                    tenancy_name = table.Column<string>(maxLength: 64, nullable: true),
                    user_id = table.Column<long>(nullable: true),
                    user_name_or_email_address = table.Column<string>(maxLength: 255, nullable: true),
                    client_ip_address = table.Column<string>(maxLength: 64, nullable: true),
                    client_name = table.Column<string>(maxLength: 128, nullable: true),
                    browser_info = table.Column<string>(maxLength: 512, nullable: true),
                    result = table.Column<byte>(nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_login_attempts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    tenant_notification_id = table.Column<Guid>(nullable: false),
                    state = table.Column<int>(nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_organization_units",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    organization_unit_id = table.Column<long>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_organization_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    authentication_source = table.Column<string>(maxLength: 64, nullable: true),
                    user_name = table.Column<string>(maxLength: 256, nullable: false),
                    tenant_id = table.Column<int>(nullable: true),
                    email_address = table.Column<string>(maxLength: 256, nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    surname = table.Column<string>(maxLength: 64, nullable: false),
                    password = table.Column<string>(maxLength: 128, nullable: false),
                    email_confirmation_code = table.Column<string>(maxLength: 328, nullable: true),
                    password_reset_code = table.Column<string>(maxLength: 328, nullable: true),
                    lockout_end_date_utc = table.Column<DateTime>(nullable: true),
                    access_failed_count = table.Column<int>(nullable: false),
                    is_lockout_enabled = table.Column<bool>(nullable: false),
                    phone_number = table.Column<string>(maxLength: 32, nullable: true),
                    is_phone_number_confirmed = table.Column<bool>(nullable: false),
                    security_stamp = table.Column<string>(maxLength: 128, nullable: true),
                    is_two_factor_enabled = table.Column<bool>(nullable: false),
                    is_email_confirmed = table.Column<bool>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: false),
                    normalized_email_address = table.Column<string>(maxLength: 256, nullable: false),
                    concurrency_stamp = table.Column<string>(maxLength: 128, nullable: true),
                    gender = table.Column<int>(nullable: false),
                    age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_users_abp_users_creator_user_id",
                        column: x => x.creator_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_users_abp_users_deleter_user_id",
                        column: x => x.deleter_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_users_abp_users_last_modifier_user_id",
                        column: x => x.last_modifier_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    phone = table.Column<string>(maxLength: 20, nullable: false),
                    detail_address = table.Column<string>(maxLength: 100, nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    title = table.Column<string>(maxLength: 50, nullable: true),
                    img = table.Column<string>(maxLength: 200, nullable: true),
                    sort = table.Column<int>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "banner",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    banner_img = table.Column<string>(nullable: true),
                    banner_link = table.Column<string>(nullable: true),
                    banner_order = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    link_type = table.Column<int>(nullable: false),
                    theme = table.Column<string>(nullable: true),
                    show_begin_date = table.Column<DateTime>(nullable: false),
                    show_end_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "laba_list",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    x = table.Column<int>(nullable: false),
                    y = table.Column<int>(nullable: false),
                    content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laba_list", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "laba_multiple",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    multiple = table.Column<int>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laba_multiple", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "laba_order_detail",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    order_id = table.Column<int>(nullable: false),
                    route_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    win_amount = table.Column<int>(nullable: false),
                    win_content = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laba_order_detail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "laba_win_route",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    x1 = table.Column<int>(nullable: false),
                    y1 = table.Column<int>(nullable: false),
                    x2 = table.Column<int>(nullable: false),
                    y2 = table.Column<int>(nullable: false),
                    x3 = table.Column<int>(nullable: false),
                    y3 = table.Column<int>(nullable: false),
                    x4 = table.Column<int>(nullable: false),
                    y4 = table.Column<int>(nullable: false),
                    x5 = table.Column<int>(nullable: false),
                    y5 = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    sequence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laba_win_route", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    brief = table.Column<string>(maxLength: 100, nullable: true),
                    category = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    sold_count = table.Column<int>(nullable: false),
                    stock = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    freight = table.Column<int>(nullable: false),
                    market_price = table.Column<int>(nullable: false),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_assets",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    gold_coin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_assets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abp_features",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    value = table.Column<string>(maxLength: 2000, nullable: false),
                    discriminator = table.Column<string>(nullable: false),
                    edition_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_features", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_features_abp_editions_edition_id",
                        column: x => x.edition_id,
                        principalTable: "abp_editions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_entity_changes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    change_time = table.Column<DateTime>(nullable: false),
                    change_type = table.Column<byte>(nullable: false),
                    entity_change_set_id = table.Column<long>(nullable: false),
                    entity_id = table.Column<string>(maxLength: 48, nullable: true),
                    entity_type_full_name = table.Column<string>(maxLength: 192, nullable: true),
                    tenant_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_entity_changes", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_entity_changes_abp_entity_change_sets_entity_change_set_~",
                        column: x => x.entity_change_set_id,
                        principalTable: "abp_entity_change_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_roles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(maxLength: 32, nullable: false),
                    display_name = table.Column<string>(maxLength: 64, nullable: false),
                    is_static = table.Column<bool>(nullable: false),
                    is_default = table.Column<bool>(nullable: false),
                    normalized_name = table.Column<string>(maxLength: 32, nullable: false),
                    concurrency_stamp = table.Column<string>(maxLength: 128, nullable: true),
                    description = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_roles_abp_users_creator_user_id",
                        column: x => x.creator_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_roles_abp_users_deleter_user_id",
                        column: x => x.deleter_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_roles_abp_users_last_modifier_user_id",
                        column: x => x.last_modifier_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_settings",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 256, nullable: false),
                    value = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_settings", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_settings_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_tenants",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    tenancy_name = table.Column<string>(maxLength: 64, nullable: false),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    connection_string = table.Column<string>(maxLength: 1024, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    edition_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_tenants", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_tenants_abp_users_creator_user_id",
                        column: x => x.creator_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_tenants_abp_users_deleter_user_id",
                        column: x => x.deleter_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_tenants_abp_editions_edition_id",
                        column: x => x.edition_id,
                        principalTable: "abp_editions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_tenants_abp_users_last_modifier_user_id",
                        column: x => x.last_modifier_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_claims",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    claim_type = table.Column<string>(maxLength: 256, nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_user_claims_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_logins",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    login_provider = table.Column<string>(maxLength: 128, nullable: false),
                    provider_key = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_logins", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_user_logins_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_roles",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_user_roles_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_tokens",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    login_provider = table.Column<string>(maxLength: 128, nullable: true),
                    name = table.Column<string>(maxLength: 128, nullable: true),
                    value = table.Column<string>(maxLength: 512, nullable: true),
                    expire_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_user_tokens_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "laba_order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    amount = table.Column<int>(nullable: false),
                    win_amount = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laba_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_laba_order_abp_users_creator_user_id",
                        column: x => x.creator_user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    order_code = table.Column<string>(maxLength: 20, nullable: false),
                    user_id = table.Column<long>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    product_name = table.Column<string>(maxLength: 50, nullable: true),
                    price = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    cut_price = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    address_id = table.Column<int>(nullable: true),
                    coupon = table.Column<string>(maxLength: 50, nullable: true),
                    express_name = table.Column<string>(maxLength: 30, nullable: true),
                    express_code = table.Column<string>(maxLength: 30, nullable: true),
                    freight = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    idx = table.Column<long>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: true),
                    contact_way = table.Column<string>(maxLength: 100, nullable: true),
                    product_type = table.Column<int>(nullable: false),
                    pay_balance = table.Column<decimal>(nullable: false),
                    pay_integration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_order_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_order_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_order_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_entity_property_changes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    entity_change_id = table.Column<long>(nullable: false),
                    new_value = table.Column<string>(maxLength: 512, nullable: true),
                    original_value = table.Column<string>(maxLength: 512, nullable: true),
                    property_name = table.Column<string>(maxLength: 96, nullable: true),
                    property_type_full_name = table.Column<string>(maxLength: 192, nullable: true),
                    tenant_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_entity_property_changes", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_entity_property_changes_abp_entity_changes_entity_change~",
                        column: x => x.entity_change_id,
                        principalTable: "abp_entity_changes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_permissions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    is_granted = table.Column<bool>(nullable: false),
                    discriminator = table.Column<string>(nullable: false),
                    role_id = table.Column<int>(nullable: true),
                    user_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_permissions_abp_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "abp_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_abp_permissions_abp_users_user_id",
                        column: x => x.user_id,
                        principalTable: "abp_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_role_claims",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    tenant_id = table.Column<int>(nullable: true),
                    role_id = table.Column<int>(nullable: false),
                    claim_type = table.Column<string>(maxLength: 256, nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_abp_role_claims_abp_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "abp_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_abp_audit_logs_tenant_id_execution_duration",
                table: "abp_audit_logs",
                columns: new[] { "tenant_id", "execution_duration" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_audit_logs_tenant_id_execution_time",
                table: "abp_audit_logs",
                columns: new[] { "tenant_id", "execution_time" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_audit_logs_tenant_id_user_id",
                table: "abp_audit_logs",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_background_jobs_is_abandoned_next_try_time",
                table: "abp_background_jobs",
                columns: new[] { "is_abandoned", "next_try_time" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_sets_tenant_id_creation_time",
                table: "abp_entity_change_sets",
                columns: new[] { "tenant_id", "creation_time" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_sets_tenant_id_reason",
                table: "abp_entity_change_sets",
                columns: new[] { "tenant_id", "reason" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_sets_tenant_id_user_id",
                table: "abp_entity_change_sets",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_changes_entity_change_set_id",
                table: "abp_entity_changes",
                column: "entity_change_set_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_changes_entity_type_full_name_entity_id",
                table: "abp_entity_changes",
                columns: new[] { "entity_type_full_name", "entity_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_property_changes_entity_change_id",
                table: "abp_entity_property_changes",
                column: "entity_change_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_features_edition_id_name",
                table: "abp_features",
                columns: new[] { "edition_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_features_tenant_id_name",
                table: "abp_features",
                columns: new[] { "tenant_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_language_texts_tenant_id_source_language_name_key",
                table: "abp_language_texts",
                columns: new[] { "tenant_id", "source", "language_name", "key" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_languages_tenant_id_name",
                table: "abp_languages",
                columns: new[] { "tenant_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_notification_subscriptions_notification_name_entity_type~",
                table: "abp_notification_subscriptions",
                columns: new[] { "notification_name", "entity_type_name", "entity_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_notification_subscriptions_tenant_id_notification_name_e~",
                table: "abp_notification_subscriptions",
                columns: new[] { "tenant_id", "notification_name", "entity_type_name", "entity_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_organization_unit_roles_tenant_id_organization_unit_id",
                table: "abp_organization_unit_roles",
                columns: new[] { "tenant_id", "organization_unit_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_organization_unit_roles_tenant_id_role_id",
                table: "abp_organization_unit_roles",
                columns: new[] { "tenant_id", "role_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_organization_units_parent_id",
                table: "abp_organization_units",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_organization_units_tenant_id_code",
                table: "abp_organization_units",
                columns: new[] { "tenant_id", "code" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_permissions_tenant_id_name",
                table: "abp_permissions",
                columns: new[] { "tenant_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_permissions_role_id",
                table: "abp_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_permissions_user_id",
                table: "abp_permissions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_claims_role_id",
                table: "abp_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_claims_tenant_id_claim_type",
                table: "abp_role_claims",
                columns: new[] { "tenant_id", "claim_type" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_roles_creator_user_id",
                table: "abp_roles",
                column: "creator_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_roles_deleter_user_id",
                table: "abp_roles",
                column: "deleter_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_roles_last_modifier_user_id",
                table: "abp_roles",
                column: "last_modifier_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_roles_tenant_id_normalized_name",
                table: "abp_roles",
                columns: new[] { "tenant_id", "normalized_name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_settings_user_id",
                table: "abp_settings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_settings_tenant_id_name_user_id",
                table: "abp_settings",
                columns: new[] { "tenant_id", "name", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_notifications_tenant_id",
                table: "abp_tenant_notifications",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenants_creator_user_id",
                table: "abp_tenants",
                column: "creator_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenants_deleter_user_id",
                table: "abp_tenants",
                column: "deleter_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenants_edition_id",
                table: "abp_tenants",
                column: "edition_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenants_last_modifier_user_id",
                table: "abp_tenants",
                column: "last_modifier_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenants_tenancy_name",
                table: "abp_tenants",
                column: "tenancy_name");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_accounts_email_address",
                table: "abp_user_accounts",
                column: "email_address");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_accounts_user_name",
                table: "abp_user_accounts",
                column: "user_name");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_accounts_tenant_id_email_address",
                table: "abp_user_accounts",
                columns: new[] { "tenant_id", "email_address" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_accounts_tenant_id_user_id",
                table: "abp_user_accounts",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_accounts_tenant_id_user_name",
                table: "abp_user_accounts",
                columns: new[] { "tenant_id", "user_name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_claims_user_id",
                table: "abp_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_claims_tenant_id_claim_type",
                table: "abp_user_claims",
                columns: new[] { "tenant_id", "claim_type" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_attempts_user_id_tenant_id",
                table: "abp_user_login_attempts",
                columns: new[] { "user_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_attempts_tenancy_name_user_name_or_email_addr~",
                table: "abp_user_login_attempts",
                columns: new[] { "tenancy_name", "user_name_or_email_address", "result" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_logins_user_id",
                table: "abp_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_logins_tenant_id_user_id",
                table: "abp_user_logins",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_logins_tenant_id_login_provider_provider_key",
                table: "abp_user_logins",
                columns: new[] { "tenant_id", "login_provider", "provider_key" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_notifications_user_id_state_creation_time",
                table: "abp_user_notifications",
                columns: new[] { "user_id", "state", "creation_time" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_organization_units_tenant_id_organization_unit_id",
                table: "abp_user_organization_units",
                columns: new[] { "tenant_id", "organization_unit_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_organization_units_tenant_id_user_id",
                table: "abp_user_organization_units",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_roles_user_id",
                table: "abp_user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_roles_tenant_id_role_id",
                table: "abp_user_roles",
                columns: new[] { "tenant_id", "role_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_roles_tenant_id_user_id",
                table: "abp_user_roles",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_tokens_user_id",
                table: "abp_user_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_tokens_tenant_id_user_id",
                table: "abp_user_tokens",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_users_creator_user_id",
                table: "abp_users",
                column: "creator_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_users_deleter_user_id",
                table: "abp_users",
                column: "deleter_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_users_last_modifier_user_id",
                table: "abp_users",
                column: "last_modifier_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_abp_users_tenant_id_normalized_email_address",
                table: "abp_users",
                columns: new[] { "tenant_id", "normalized_email_address" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_users_tenant_id_normalized_user_name",
                table: "abp_users",
                columns: new[] { "tenant_id", "normalized_user_name" });

            migrationBuilder.CreateIndex(
                name: "IX_laba_order_creator_user_id",
                table: "laba_order",
                column: "creator_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_order_address_id",
                table: "product_order",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_order_product_id",
                table: "product_order",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_order_user_id",
                table: "product_order",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abp_audit_logs");

            migrationBuilder.DropTable(
                name: "abp_background_jobs");

            migrationBuilder.DropTable(
                name: "abp_entity_property_changes");

            migrationBuilder.DropTable(
                name: "abp_features");

            migrationBuilder.DropTable(
                name: "abp_language_texts");

            migrationBuilder.DropTable(
                name: "abp_languages");

            migrationBuilder.DropTable(
                name: "abp_notification_subscriptions");

            migrationBuilder.DropTable(
                name: "abp_notifications");

            migrationBuilder.DropTable(
                name: "abp_organization_unit_roles");

            migrationBuilder.DropTable(
                name: "abp_organization_units");

            migrationBuilder.DropTable(
                name: "abp_permissions");

            migrationBuilder.DropTable(
                name: "abp_role_claims");

            migrationBuilder.DropTable(
                name: "abp_settings");

            migrationBuilder.DropTable(
                name: "abp_tenant_notifications");

            migrationBuilder.DropTable(
                name: "abp_tenants");

            migrationBuilder.DropTable(
                name: "abp_user_accounts");

            migrationBuilder.DropTable(
                name: "abp_user_claims");

            migrationBuilder.DropTable(
                name: "abp_user_login_attempts");

            migrationBuilder.DropTable(
                name: "abp_user_logins");

            migrationBuilder.DropTable(
                name: "abp_user_notifications");

            migrationBuilder.DropTable(
                name: "abp_user_organization_units");

            migrationBuilder.DropTable(
                name: "abp_user_roles");

            migrationBuilder.DropTable(
                name: "abp_user_tokens");

            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "banner");

            migrationBuilder.DropTable(
                name: "laba_list");

            migrationBuilder.DropTable(
                name: "laba_multiple");

            migrationBuilder.DropTable(
                name: "laba_order");

            migrationBuilder.DropTable(
                name: "laba_order_detail");

            migrationBuilder.DropTable(
                name: "laba_win_route");

            migrationBuilder.DropTable(
                name: "product_order");

            migrationBuilder.DropTable(
                name: "user_assets");

            migrationBuilder.DropTable(
                name: "abp_entity_changes");

            migrationBuilder.DropTable(
                name: "abp_roles");

            migrationBuilder.DropTable(
                name: "abp_editions");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "abp_entity_change_sets");

            migrationBuilder.DropTable(
                name: "abp_users");
        }
    }
}
