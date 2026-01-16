-- ------------------------------------------------------
-- Unified Database Structure Dump: loot_trade
-- ------------------------------------------------------

-- Disable foreign key checks to avoid dependency issues
SET FOREIGN_KEY_CHECKS = 0;

-- ------------------------------------------------------
-- TABLE: role
-- ------------------------------------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `title` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: user
-- ------------------------------------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `roleId` INT NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`),
  KEY `FKrole_idx` (`roleId`),
  CONSTRAINT `FKrole` FOREIGN KEY (`roleId`) REFERENCES `role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: game
-- ------------------------------------------------------
DROP TABLE IF EXISTS `game`;
CREATE TABLE `game` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `title` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: item
-- ------------------------------------------------------
DROP TABLE IF EXISTS `item`;
CREATE TABLE `item` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `gameId` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `description` VARCHAR(45),
  `image` BLOB,
  PRIMARY KEY (`id`),
  KEY `gameId_idx` (`gameId`),
  CONSTRAINT `FKGame` FOREIGN KEY (`gameId`) REFERENCES `game` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: inventory
-- ------------------------------------------------------
DROP TABLE IF EXISTS `inventory`;
CREATE TABLE `inventory` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `itemId` INT NOT NULL,
  `userId` INT NOT NULL,
  PRIMARY KEY (`id`),
  KEY `itemId_idx` (`itemId`),
  KEY `userId_idx` (`userId`),
  CONSTRAINT `FKItem` FOREIGN KEY (`itemId`) REFERENCES `item` (`id`),
  CONSTRAINT `FKUser` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: offered
-- ------------------------------------------------------
DROP TABLE IF EXISTS `offered`;
CREATE TABLE `offered` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `inventoryId` INT NOT NULL,
  `dateTimeOpen` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  KEY `inventoryId_idx` (`inventoryId`),
  CONSTRAINT `inventoryIdFK` FOREIGN KEY (`inventoryId`) REFERENCES `inventory` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: trade
-- ------------------------------------------------------
DROP TABLE IF EXISTS `trade`;
CREATE TABLE `trade` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `offeredId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `offeredIdFK_idx` (`offeredId`),
  CONSTRAINT `offeredIdFK` FOREIGN KEY (`offeredId`) REFERENCES `offered` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: trade_item
-- ------------------------------------------------------
DROP TABLE IF EXISTS `trade_item`;
CREATE TABLE `trade_item` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `tradeId` INT NOT NULL,
  `inventoryId` INT NOT NULL,
  PRIMARY KEY (`id`),
  KEY `tradeIdFK_idx` (`tradeId`),
  KEY `inventoryIdFK_idx` (`inventoryId`),
  CONSTRAINT `tradeIdFK` FOREIGN KEY (`tradeId`) REFERENCES `trade` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `inventoryIdTradeItemFK` FOREIGN KEY (`inventoryId`) REFERENCES `inventory` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ------------------------------------------------------
-- TABLE: accepted_trade
-- ------------------------------------------------------
DROP TABLE IF EXISTS `accepted_trade`;
CREATE TABLE `accepted_trade` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `tradeId` INT NOT NULL,
  `offeredId` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idpast_trade_UNIQUE` (`id`),
  KEY `accepted_tradeOfferedFKId_idx` (`offeredId`),
  KEY `accepted_tradeTradeIdFK_idx` (`tradeId`),
  CONSTRAINT `accepted_tradeTradeIdFK` FOREIGN KEY (`tradeId`) REFERENCES `trade` (`Id`),
  CONSTRAINT `accepted_tradeOfferedIdFK` FOREIGN KEY (`offeredId`) REFERENCES `offered` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;
