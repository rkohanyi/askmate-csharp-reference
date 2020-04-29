DROP TABLE IF EXISTS question_tag;

CREATE TABLE question_tag (
  question_id INTEGER NOT NULL REFERENCES question(id),
  tag_id INTEGER NOT NULL REFERENCES tag(id)
);

INSERT INTO question_tag (question_id,tag_id) VALUES (73,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (83,10);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (85,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (67,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (69,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (63,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,41);
INSERT INTO question_tag (question_id,tag_id) VALUES (41,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (9,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,16);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (1,40);
INSERT INTO question_tag (question_id,tag_id) VALUES (91,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (75,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (80,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (10,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (2,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (37,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (87,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (41,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (100,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (88,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (20,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (93,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (73,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (12,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (65,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,40);
INSERT INTO question_tag (question_id,tag_id) VALUES (65,41);
INSERT INTO question_tag (question_id,tag_id) VALUES (3,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (55,41);
INSERT INTO question_tag (question_id,tag_id) VALUES (1,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (55,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (34,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,40);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (39,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,10);
INSERT INTO question_tag (question_id,tag_id) VALUES (86,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (30,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (79,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (16,2);
INSERT INTO question_tag (question_id,tag_id) VALUES (42,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,13);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (86,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (60,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (15,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (26,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (65,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (21,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (100,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (52,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (100,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (7,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (27,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (9,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (36,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (61,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (55,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (21,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (67,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (56,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,40);
INSERT INTO question_tag (question_id,tag_id) VALUES (12,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (15,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (67,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (34,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (32,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (13,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (13,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (12,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (63,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (11,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (51,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (31,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (72,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (55,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (73,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (23,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (29,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,50);
INSERT INTO question_tag (question_id,tag_id) VALUES (51,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (66,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (11,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (2,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (60,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (75,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (41,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (35,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (85,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (86,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (68,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (51,50);
INSERT INTO question_tag (question_id,tag_id) VALUES (32,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (68,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (5,2);
INSERT INTO question_tag (question_id,tag_id) VALUES (20,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (69,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,36);
INSERT INTO question_tag (question_id,tag_id) VALUES (12,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (42,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (90,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (9,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (61,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (81,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (94,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (52,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (48,2);
INSERT INTO question_tag (question_id,tag_id) VALUES (78,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (37,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (45,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (5,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (39,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (67,41);
INSERT INTO question_tag (question_id,tag_id) VALUES (83,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (3,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (60,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (86,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (91,16);
INSERT INTO question_tag (question_id,tag_id) VALUES (67,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (88,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,36);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (70,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (52,16);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (75,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (49,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (31,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (75,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (25,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (13,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (40,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (94,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (66,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (29,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (42,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (72,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (87,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (20,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (17,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (87,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (27,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (56,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (37,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (91,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (1,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (1,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (72,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (61,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (26,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (69,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (29,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (94,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (7,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (14,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (98,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (85,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (40,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (54,36);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (78,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (25,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (52,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (17,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (98,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (37,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (75,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (52,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (56,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (30,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (3,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (23,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (66,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (57,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (69,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (56,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (72,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (14,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (27,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (94,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (5,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (21,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (54,10);
INSERT INTO question_tag (question_id,tag_id) VALUES (39,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (80,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (85,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (31,13);
INSERT INTO question_tag (question_id,tag_id) VALUES (23,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (87,2);
INSERT INTO question_tag (question_id,tag_id) VALUES (24,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,36);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (64,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (36,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (25,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (30,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (10,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (83,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (36,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (20,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (16,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (86,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (3,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (70,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (81,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (45,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (25,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (24,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (26,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (23,50);
INSERT INTO question_tag (question_id,tag_id) VALUES (9,2);
INSERT INTO question_tag (question_id,tag_id) VALUES (42,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (54,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (10,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (39,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (40,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (98,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (45,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (72,13);
INSERT INTO question_tag (question_id,tag_id) VALUES (80,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (20,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (64,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (83,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (21,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (66,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (78,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (64,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (17,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (11,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (29,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (70,37);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (18,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (5,13);
INSERT INTO question_tag (question_id,tag_id) VALUES (53,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (10,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (22,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (24,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (49,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (10,50);
INSERT INTO question_tag (question_id,tag_id) VALUES (36,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (50,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (79,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,41);
INSERT INTO question_tag (question_id,tag_id) VALUES (8,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (33,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (14,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (81,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (5,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (93,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (69,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (20,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (94,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (24,11);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,50);
INSERT INTO question_tag (question_id,tag_id) VALUES (1,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (90,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (17,50);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (88,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (91,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (54,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (93,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (6,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (72,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (32,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (63,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (85,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,38);
INSERT INTO question_tag (question_id,tag_id) VALUES (92,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (36,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (66,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (83,48);
INSERT INTO question_tag (question_id,tag_id) VALUES (77,44);
INSERT INTO question_tag (question_id,tag_id) VALUES (10,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (88,29);
INSERT INTO question_tag (question_id,tag_id) VALUES (54,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (2,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (75,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (73,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,39);
INSERT INTO question_tag (question_id,tag_id) VALUES (51,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (29,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (25,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (29,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (58,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (32,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (5,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (62,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (40,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (32,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (56,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (38,7);
INSERT INTO question_tag (question_id,tag_id) VALUES (1,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (60,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (96,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (11,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (19,35);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (77,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (2,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (49,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (100,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (16,22);
INSERT INTO question_tag (question_id,tag_id) VALUES (97,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (50,28);
INSERT INTO question_tag (question_id,tag_id) VALUES (57,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (13,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (59,34);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (93,20);
INSERT INTO question_tag (question_id,tag_id) VALUES (95,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (76,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (37,26);
INSERT INTO question_tag (question_id,tag_id) VALUES (3,19);
INSERT INTO question_tag (question_id,tag_id) VALUES (16,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (9,27);
INSERT INTO question_tag (question_id,tag_id) VALUES (82,14);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (54,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (35,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (44,3);
INSERT INTO question_tag (question_id,tag_id) VALUES (2,17);
INSERT INTO question_tag (question_id,tag_id) VALUES (40,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (11,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (65,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (35,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (99,40);
INSERT INTO question_tag (question_id,tag_id) VALUES (86,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (27,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (83,2);
INSERT INTO question_tag (question_id,tag_id) VALUES (27,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (65,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (84,24);
INSERT INTO question_tag (question_id,tag_id) VALUES (81,8);
INSERT INTO question_tag (question_id,tag_id) VALUES (15,4);
INSERT INTO question_tag (question_id,tag_id) VALUES (52,46);
INSERT INTO question_tag (question_id,tag_id) VALUES (48,33);
INSERT INTO question_tag (question_id,tag_id) VALUES (28,42);
INSERT INTO question_tag (question_id,tag_id) VALUES (45,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (66,18);
INSERT INTO question_tag (question_id,tag_id) VALUES (27,5);
INSERT INTO question_tag (question_id,tag_id) VALUES (80,30);
INSERT INTO question_tag (question_id,tag_id) VALUES (73,32);
INSERT INTO question_tag (question_id,tag_id) VALUES (91,9);
INSERT INTO question_tag (question_id,tag_id) VALUES (25,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (40,47);
INSERT INTO question_tag (question_id,tag_id) VALUES (13,41);
INSERT INTO question_tag (question_id,tag_id) VALUES (4,36);
INSERT INTO question_tag (question_id,tag_id) VALUES (98,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (48,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,49);
INSERT INTO question_tag (question_id,tag_id) VALUES (74,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (70,6);
INSERT INTO question_tag (question_id,tag_id) VALUES (71,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (68,40);
INSERT INTO question_tag (question_id,tag_id) VALUES (7,23);
INSERT INTO question_tag (question_id,tag_id) VALUES (30,43);
INSERT INTO question_tag (question_id,tag_id) VALUES (46,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (35,21);
INSERT INTO question_tag (question_id,tag_id) VALUES (57,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (61,15);
INSERT INTO question_tag (question_id,tag_id) VALUES (34,31);
INSERT INTO question_tag (question_id,tag_id) VALUES (69,1);
INSERT INTO question_tag (question_id,tag_id) VALUES (63,45);
INSERT INTO question_tag (question_id,tag_id) VALUES (7,13);
INSERT INTO question_tag (question_id,tag_id) VALUES (47,25);
INSERT INTO question_tag (question_id,tag_id) VALUES (3,12);
INSERT INTO question_tag (question_id,tag_id) VALUES (65,36);