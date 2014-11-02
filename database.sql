-- MySQL Script generated by MySQL Workbench
-- 10/17/14 12:11:14
-- Model: New Model    Version: 1.0
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema intosport
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `intosport` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `intosport` ;

-- -----------------------------------------------------
-- Table `intosport`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `voornaam` VARCHAR(45) NULL,
  `achternaam` VARCHAR(45) NULL,
  `adres` VARCHAR(45) NULL,
  `huisnr` VARCHAR(45) NULL,
  `postcode` VARCHAR(6) NULL,
  `tel` VARCHAR(45) NULL,
  `plaats` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `wachtwoord` VARCHAR(45) NULL,
  `role` ENUM('klant', 'beheerder', 'manager') NULL DEFAULT 'klant',
  `goldmember` TINYINT(1) NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `email_UNIQUE` (`email` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`bestelling`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`bestelling` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `status` ENUM('in_behandeling', 'betaald', 'verstuurd', 'vervallen') NULL,
  `datum` VARCHAR(45) NOT NULL,
  `korting` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `user_id_idx` (`user_id` ASC),
  CONSTRAINT `user_id`
    FOREIGN KEY (`user_id`)
    REFERENCES `intosport`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`product` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `naam` VARCHAR(45) NOT NULL,
  `prijs` DOUBLE NOT NULL,
  `korting` INT NULL,
  `voorraad` INT NOT NULL DEFAULT 0,
  `afbeelding` VARCHAR(45) NULL,
  `thumbnail` VARCHAR(45) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`order_regel`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`order_regel` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `bestelling_id` INT NOT NULL,
  `product_id` INT NOT NULL,
  `hoeveelheid` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `bestelling_id_idx` (`bestelling_id` ASC),
  INDEX `product_id_idx` (`product_id` ASC),
  CONSTRAINT `bestelling_id`
    FOREIGN KEY (`bestelling_id`)
    REFERENCES `intosport`.`bestelling` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `product_id`
    FOREIGN KEY (`product_id`)
    REFERENCES `intosport`.`product` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`categorie`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`categorie` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `parent` INT NULL,
  `naam` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`product_categorie`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`product_categorie` (
  `product_id` INT NOT NULL,
  `categorie_id` INT NOT NULL,
  PRIMARY KEY (`product_id`, `categorie_id`),
  INDEX `categorie_id_idx` (`categorie_id` ASC),
  CONSTRAINT `product2_id`
    FOREIGN KEY (`product_id`)
    REFERENCES `intosport`.`product` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `categorie_id`
    FOREIGN KEY (`categorie_id`)
    REFERENCES `intosport`.`categorie` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`detail`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`detail` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `naam` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`detail_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`detail_product` (
  `product_id` INT NOT NULL,
  `detail_id` INT NOT NULL,
  PRIMARY KEY (`product_id`, `detail_id`),
  INDEX `detail_id_idx` (`detail_id` ASC),
  CONSTRAINT `product3_id`
    FOREIGN KEY (`product_id`)
    REFERENCES `intosport`.`product` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `detail_id`
    FOREIGN KEY (`detail_id`)
    REFERENCES `intosport`.`detail` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `intosport`.`detail_waarde`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `intosport`.`detail_waarde` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `detail_id` INT NOT NULL,
  `waarde` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `detail_id_idx` (`detail_id` ASC),
  CONSTRAINT `detail2_id`
    FOREIGN KEY (`detail_id`)
    REFERENCES `intosport`.`detail` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
