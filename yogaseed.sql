USE [SoulFly];
GO

SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([id], [displayName], [email], [password], [birthday])
VALUES
  (1, 'John Doe', 'john.doe@example.com', 'password', '1990-01-01'),
  (2, 'Jane Smith', 'sample@sample.com', 'password', '1985-05-15'),
  (3, 'Alex Johnson', 'example@example.com', 'password', '1995-09-30');
SET IDENTITY_INSERT [Users] OFF;

SET ANSI_WARNINGS OFF;
SET IDENTITY_INSERT [Poses] ON;
INSERT INTO [Poses] ([id], [name], [description], [image])
VALUES
  (9, 'Chair', 'From a standing position, the feet are together and rooted into the earth with toes actively lifted. The knees are bent and the weight of the body is on the heels of the feet. The pelvis is tucked in and the ribcage is lifted. The neck is a natural extension of the spine. The arms are lifted up toward the sky with the elbows straight and the biceps by the ears. The hands can be together or separated and facing each other with the fingers spread wide. The gaze is forward.', 'https://res.cloudinary.com/dko1be2jy/image/upload/fl_sanitize/v1676483078/yoga-api/9_ewvoun.png'),
  (10, 'Childs Pose','From a kneeling position, the toes and knees are together with most of the weight of the body resting on the heels of the feet. The arms are extended back resting alongside the legs. The forehead rests softly onto the earth. The gaze is down and inward.', 'https://res.cloudinary.com/dko1be2jy/image/upload/fl_sanitize/v1676483079/yoga-api/10_wzpo85.png'),
  (11, 'Corpse','The body rests on the earth in a supine position with the arms resting by the side body. The palms are relaxed and open toward the sky. The shoulder blades are pulled back, down and rolled under comfortably, resting evenly on the earth. The legs are extended down and splayed open. The heels are in and the toes flop out. The eyes are closed. Everything is relaxed. The gaze is inward.', 'https://res.cloudinary.com/dko1be2jy/image/upload/fl_sanitize/v1676483078/yoga-api/11_dczyrp.png');
SET IDENTITY_INSERT [Poses] OFF;
SET ANSI_WARNINGS ON;

SET IDENTITY_INSERT [Routine] ON;
INSERT INTO [Routine] ([id], [userId], [intention], [poseId], [cycles], [creationDate], [reflection])
VALUES
  (1, 1, 'Wake up and stretch your body.', 9, 3, '2023-07-15', 'Felt great!'),
  (2, 2, 'Relax and unwind before bedtime.', 11, 2, '2023-07-16', 'Feeling calm.'),
  (3, 3, 'Focus on building core strength.', 10, 5, '2023-07-17', 'Tough but rewarding');
SET IDENTITY_INSERT [Routine] OFF;

SET IDENTITY_INSERT [RoutinePoses] ON;
INSERT INTO [RoutinePoses] ([id], [routineId], [poseId], [commentId])
VALUES
  (1, 1, 9, NULL),
  (2, 1, 10, NULL),
  (3, 1, 11, NULL),
  (4, 2, 10, NULL),
  (5, 2, 9, NULL),
  (6, 3, 11, NULL),
  (7, 3, 10, NULL),
  (8, 3, 9, NULL);
SET IDENTITY_INSERT [RoutinePoses] OFF;

SET IDENTITY_INSERT [Comment] ON;
INSERT INTO [Comment] ([id], [userId], [routineId], [text])
VALUES
  (1, 1, 1, 'Great routine to start the day!'),
  (2, 2, 1, 'I enjoyed the stretches.'),
  (3, 1, 2, 'Downward Dog is my favorite pose.'),
  (4, 3, 2, 'Helps me relax before bed.'),
  (5, 1, 3, 'Challenging, but worth it.');
SET IDENTITY_INSERT [Comment] OFF;