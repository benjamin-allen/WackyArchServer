/* 
-- FLAG{thanks_for_reading_this_far}
insert into BetaChallenges (Flag, PredecessorId, Name, Category, Description, [Order], InputProgramJson)
values ('You can''t find the flag this way', null, 'Inspected by no. 4FF', 'Beta', 'Find the flag.', 100, '[15, 1289, 3940, 688, 172, 188, 236, 1543, 1286, 1529, 15, 15, 15, 1532, 960, 772, 77, 65, 73, 78, 1027, 4095, 562, 1541, 3841, 176, 34, 1529, 768, 1279, 70, 76, 65, 71, 123, 116, 104, 97, 110, 107, 115, 95, 102, 111, 114, 95, 114, 101, 97, 100, 105, 110, 103, 95, 116, 104, 105, 115, 95, 102, 97, 114, 125]')
*/

/*
-- Answer; 0x46 0x99 0x20 0x0f 0xe2 0xf1 0x9f 0xaa
insert into BetaChallenges (Flag, PredecessorId, Name, Category, Description, [Order], InputProgramJson)
values ('FLAG{B00YEAH}', null, 'CRC Lock', 'Beta', 'This is an intact piece of code that requires a particular set of 8 inputs to decrypt additional contents in the Red Queen''s machine. Something about a CRC?

I think you''ll need to bust out the scripting for this one.', 200, '[963, 772, 67, 82, 67, 52, 3859, 560, 263, 3840, 690, 1794, 140, 962, 1798, 140, 3856, 690, 2054, 1530, 385, 2817, 2046, 1528, 1008, 768, 776, 80, 65, 68, 52, 66, 73, 84, 83, 1200, 4095, 186, 292, 1072, 1008, 768, 782, 71, 69, 84, 66, 73, 84, 83, 84, 79, 65, 76, 73, 71, 78, 1152, 1168, 1184, 92, 3840, 690, 2053, 289, 3841, 112, 1530, 3840, 562, 2053, 257, 3841, 113, 1530, 720, 1056, 1040, 1024, 1008, 768, 772, 77, 65, 73, 78, 1059, 608, 1168, 961, 960, 3853, 690, 2129, 1872, 1059, 1040, 96, 1168, 961, 960, 3843, 690, 2119, 1862, 1059, 1040, 96, 1168, 961, 960, 3850, 690, 2109, 1852, 1059, 1040, 96, 1168, 961, 960, 3842, 690, 2099, 1842, 1059, 1040, 96, 1168, 961, 960, 3845, 690, 2089, 1832, 1059, 1040, 96, 1168, 961, 960, 3845, 690, 2079, 1822, 1059, 1040, 96, 1168, 961, 960, 3849, 690, 2069, 1812, 1059, 1040, 96, 1168, 961, 960, 3849, 690, 2059, 1802, 1040, 3840, 2815, 2815, 2815, 2815, 2606, 626, 1538, 1265, 1264, 768, 1279]')
*/

/*
insert into BetaChallenges (Flag, PredecessorId, Name, Category, Description, [Order], InputProgramJson)
values ('FLAG{where_were_going_we_dont_need_write_protection}', null, 'Sh0rt Expl01t', 'Beta', 'How dangerous can 4 little instructions be?', 50, '[1027, 3844, 1201, 1154, 1279]')
*/

/*
insert into BetaChallenges (Flag, PredecessorId, Name, Category, Description, [Order], InputProgramJson)
values ('FLAG{l1k3_c4ndy_fr0m_4_b4by}', null, 'Bad Call', 'Beta', 'Is this some kind of pun?', 75, '[960, 1279, 772, 77, 65, 73, 78, 1027, 3855, 58, 3900, 308, 59, 3856, 1201, 1154, 15, 15, 768, 770, 70, 49, 1265, 768, 770, 70, 50, 1265, 768, 770, 70, 51, 1265, 768, 770, 70, 52, 1265, 768, 770, 70, 53, 1265, 768, 770, 70, 54, 1265, 768, 770, 70, 55, 1264, 768, 770, 70, 56, 1265, 768, 1279]')
*/

/*
-- Answer: Any 32 words followed by ffe, ffe, and then some more data to replace the 3C9 instruction with 3C8.
insert into BetaChallenges (Flag, PredecessorId, Name, Category, Description, [Order], InputProgramJson)
values ('FLAG{cl3v3r_g1rl...}', (select Id from BetaChallenges where Name = 'Sh0rt Expl01t'), 'L0ng Expl01t', 'Beta', 'Don''t think I don''t see what you''re up to! But I''ve added some more memory safety after your last little exploit! Don''t count on pulling off Sh0rt Expl01t again!', 500, '[970, 1279, 778, 68, 69, 66, 85, 71, 72, 79, 79, 75, 49, 1152, 1024, 1168, 1040, 1184, 1056, 1200, 1072, 768, 778, 68, 69, 66, 85, 71, 72, 79, 79, 75, 50, 1025, 1042, 28, 28, 12, 92, 768, 773, 69, 82, 82, 79, 82, 1265, 768, 777, 82, 69, 65, 68, 73, 78, 80, 85, 84, 1152, 1168, 1200, 3840, 2815, 624, 1027, 4095, 562, 1543, 3841, 1169, 1154, 964, 113, 1527, 1072, 1040, 1024, 1008, 768, 779, 83, 65, 70, 69, 84, 89, 67, 72, 69, 67, 75, 1152, 1200, 3840, 2783, 1201, 1058, 4095, 308, 3598, 690, 1538, 962, 3840, 2782, 1201, 1058, 4095, 308, 3598, 690, 1538, 962, 1169, 1072, 1024, 1008, 768, 777, 83, 65, 70, 69, 84, 89, 83, 69, 84, 1200, 3840, 2783, 1201, 4095, 308, 3598, 1202, 3840, 2782, 1201, 4095, 308, 3598, 1202, 1072, 1008, 768, 779, 85, 78, 76, 79, 67, 75, 77, 69, 78, 79, 87, 768, 778, 78, 79, 73, 77, 85, 78, 76, 79, 67, 75, 768, 781, 85, 78, 76, 79, 67, 75, 84, 72, 73, 83, 79, 78, 69, 1264, 768, 780, 80, 82, 79, 67, 69, 83, 83, 73, 78, 80, 85, 84, 1265, 768, 772, 77, 65, 73, 78, 2570, 560, 624, 274, 449, 965, 963, 969, 15, 15, 15, 15, 1008, 768, 1279]')
*/

-- Answer: 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, fff
/*
insert into BetaChallenges (Flag, PredecessorId, Name, Category, Description, [Order], InputProgramJson)
values ('FLAG{!!!___BTF0___!!!}', (select Id from BetaChallenges where Name = 'Bad Call'), 'STACK SMAAAAASH', 'Beta', 'Who you gonna call?', 400, '[963, 1279, 774, 76, 65, 89, 69, 82, 49, 3840, 2815, 176, 2818, 2050, 1533, 961, 1264, 768, 774, 76, 65, 89, 69, 82, 50, 688, 3111, 962, 1265, 768, 778, 82, 69, 65, 68, 73, 78, 80, 85, 84, 83, 1027, 4095, 562, 1540, 2051, 1152, 1530, 1008, 768, 772, 77, 65, 73, 78, 960, 1008, 768, 1279]')
*/

select * from BetaChallenges