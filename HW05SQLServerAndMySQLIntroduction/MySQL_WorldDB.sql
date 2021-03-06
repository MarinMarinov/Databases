-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema world
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema world
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `world` DEFAULT CHARACTER SET utf8 ;
USE `world` ;

-- -----------------------------------------------------
-- Table `world`.`city`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `world`.`city` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `Name` CHAR(35) NOT NULL DEFAULT '' COMMENT '',
  `CountryCode` CHAR(3) NOT NULL DEFAULT '' COMMENT '',
  `District` CHAR(20) NOT NULL DEFAULT '' COMMENT '',
  `Population` INT(11) NOT NULL DEFAULT '0' COMMENT '',
  PRIMARY KEY (`ID`)  COMMENT '')
ENGINE = MyISAM
AUTO_INCREMENT = 4080
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `world`.`country`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `world`.`country` (
  `Code` CHAR(3) NOT NULL DEFAULT '' COMMENT '',
  `Name` CHAR(52) NOT NULL DEFAULT '' COMMENT '',
  `Continent` ENUM('Asia','Europe','North America','Africa','Oceania','Antarctica','South America') NOT NULL DEFAULT 'Asia' COMMENT '',
  `Region` CHAR(26) NOT NULL DEFAULT '' COMMENT '',
  `SurfaceArea` FLOAT(10,2) NOT NULL DEFAULT '0.00' COMMENT '',
  `IndepYear` SMALLINT(6) NULL DEFAULT NULL COMMENT '',
  `Population` INT(11) NOT NULL DEFAULT '0' COMMENT '',
  `LifeExpectancy` FLOAT(3,1) NULL DEFAULT NULL COMMENT '',
  `GNP` FLOAT(10,2) NULL DEFAULT NULL COMMENT '',
  `GNPOld` FLOAT(10,2) NULL DEFAULT NULL COMMENT '',
  `LocalName` CHAR(45) NOT NULL DEFAULT '' COMMENT '',
  `GovernmentForm` CHAR(45) NOT NULL DEFAULT '' COMMENT '',
  `HeadOfState` CHAR(60) NULL DEFAULT NULL COMMENT '',
  `Capital` INT(11) NULL DEFAULT NULL COMMENT '',
  `Code2` CHAR(2) NOT NULL DEFAULT '' COMMENT '',
  PRIMARY KEY (`Code`)  COMMENT '')
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `world`.`countrylanguage`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `world`.`countrylanguage` (
  `CountryCode` CHAR(3) NOT NULL DEFAULT '' COMMENT '',
  `Language` CHAR(30) NOT NULL DEFAULT '' COMMENT '',
  `IsOfficial` ENUM('T','F') NOT NULL DEFAULT 'F' COMMENT '',
  `Percentage` FLOAT(4,1) NOT NULL DEFAULT '0.0' COMMENT '',
  PRIMARY KEY (`CountryCode`, `Language`)  COMMENT '')
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
