insert into AlphaChallenges (Flag, PredecessorId, Description, InputTextJson, OutputTextJson, Category, Name, [Order])
values ('FLAG{2_many_zeros}', NULL, 'A portion of the Rom in The Machine has been corrupted and needs to be rewritten; you''ll need to deduce the output of this function and implement it yourself.
Call this function f. f(0) = (12,0), and f(1) = f(2) = f(4) = (11,1). f(42) = (9,3).

Inputs for f arrive in the INPUT port. The output is a pair of values; write the first to F0 and the second to F1',
'[{"Name":"INPUT", "Data":[0, 1, 2, 4, 42]}]', '[{"Name":"F0", "Data":[12, 11, 11, 11, 9]}, {"Name":"F1", "Data":[0, 1, 1, 1, 3]}]',
'The Machine', 'A bit more broken', 0)

insert into AlphaChallengeTests (AlphaChallengeId, InputTextJson, OutputTextJson) values 
('8BF70D54-17D8-454B-841A-48E42B7D4B21', '[{"Name":"INPUT", "Data":[1294, 977, 43, 1951, 1199, 1324, 361, 69, 688, 53]}]', '[{"Name":"F0", "Data":[7, 6, 8, 3, 5, 7, 7, 9, 8, 8]}, {"Name":"F1", "Data":[5, 6, 4, 9, 7, 5, 5, 3, 4, 4]}]'),
('8BF70D54-17D8-454B-841A-48E42B7D4B21', '[{"Name":"INPUT", "Data":[1451, 1872, 814, 1552, 868, 1721, 1055, 1062, 1484, 357, 58, 1543, 154, 191, 709, 1692, 1148, 968, 418, 872]}]', '[{"Name":"F0", "Data":[5, 7, 6, 9, 7, 5, 6, 8, 6, 7, 8, 7, 8, 5, 7, 6, 6, 7, 8, 7]}, {"Name":"F1", "Data":[7, 5, 6, 3, 5, 7, 6, 4, 6, 5, 4, 5, 4, 7, 5, 6, 6, 5, 4, 5]}] '),
('8BF70D54-17D8-454B-841A-48E42B7D4B21', '[{"Name":"INPUT", "Data":[112, 456, 1585, 265]}]', '[{"Name":"F0", "Data":[9, 8, 7, 9]}, {"Name":"F1", "Data":[3, 4, 5, 3]}]'),
('8BF70D54-17D8-454B-841A-48E42B7D4B21', '[{"Name":"INPUT", "Data":[-1, 0]}]', '[{"Name":"F0", "Data":[0,12]}, {"Name":"F1", "Data":[12,0]}]')



select * from AlphaChallenges order by Category, [Order]
select a.*, Name from AlphaChallengeTests a join AlphaChallenges b on b.Id = a.AlphaChallengeId order by b.Category, b.[Order]
select * from CompletedAlphaChallenges