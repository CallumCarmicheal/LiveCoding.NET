CREATE TABLE `cache_store` (
  `id` int(255) NOT NULL,
  `guid` varchar(40) NOT NULL,
  `token` varchar(255) NOT NULL,
  `refresh` varchar(255) NOT NULL DEFAULT '',
  `valid` tinyint(4) NOT NULL DEFAULT '0',
  `expires` timestamp NOT NULL,
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

ALTER TABLE `cache_store` ADD PRIMARY KEY (`id`);