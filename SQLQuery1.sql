INSERT INTO AspNetUserRoles
(UserId, RoleId)
VALUES ('039cff19-267c-434f-a0a3-bbdddef8ccd5','20b023c1-0686-48a3-9f02-25b8b25ddb70'),
('190e9d99-dd0e-43e6-91b6-64705fbb2a34','8f5ba028-5877-4a24-8b5f-b9e781b1ea50'),
('7629047d-97e8-452e-8c76-55fd0385e153','f521c36c-545a-4f54-96d8-8fb274bce09a')

SELECT TOP (1000) [Id],[Name],[NormalizedName],[ConcurrencyStamp]
FROM [RestaurantDb].[dbo].[AspNetRoles]