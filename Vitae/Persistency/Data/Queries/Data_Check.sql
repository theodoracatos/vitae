--361 NO AT3, 366 REGISTERED

DECLARE @SHOWALL bit = 1
DECLARE @MAIL varchar(30) = 'at3@gmx.ch'
DECLARE @ID uniqueidentifier = (select id from dbo.aspnetusers where email = @MAIL)
DECLARE @CID uniqueidentifier = (select claimvalue from dbo.AspNetUserClaims where userid = @ID)
DECLARE @PID uniqueidentifier = (select PersonalDetailID from dbo.PersonalDetail where CurriculumID = @CID)
DECLARE @AID uniqueidentifier = (select AboutID from dbo.About where CurriculumID = @CID)

select * from dbo.AspNetUserClaims where userid = @ID or @SHOWALL = 1
select * from dbo.AspNetUserRoles where userid = @ID or @SHOWALL = 1
select * from dbo.AspNetUsers where id = @ID or @SHOWALL = 1

select * from dbo.About where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Abroad where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Award where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Certificate where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Child where PersonalDetailID = @PID or @SHOWALL = 1
select * from dbo.Course where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Curriculum where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.CurriculumLanguage where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Education where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Experience where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Interest where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.LanguageSkill where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.PersonalDetail where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.PersonCountry where PersonalDetailID = @PID or @SHOWALL = 1
select * from dbo.Publication where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Reference where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Skill where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.SocialLink where CurriculumID = @CID or @SHOWALL = 1
select * from dbo.Vfile v left join dbo.About a on a.VfileID = v.vfileid where a.AboutID = @AID or @SHOWALL = 1


--select * from dbo.AspNetRoleClaims
--select * from dbo.AspNetRoles
--select * from dbo.AspNetUserLogins
--select * from dbo.AspNetUserTokens
--select * from dbo.Log
--select * from dbo.MaritalStatus
--select * from dbo.Month
--select * from dbo.Language
--select * from dbo.HierarchyLevel
--select * from dbo.Industry
--select * from dbo.Country