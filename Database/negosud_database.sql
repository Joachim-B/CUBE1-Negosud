CREATE DATABASE  IF NOT EXISTS `negosud` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `negosud`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: negosud
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `article`
--

DROP TABLE IF EXISTS `article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article` (
  `IDArticle` int NOT NULL AUTO_INCREMENT,
  `Reference` varchar(256) DEFAULT NULL,
  `Name` varchar(256) NOT NULL,
  `WineYear` int NOT NULL,
  `UnitPriceTTC` decimal(15,2) NOT NULL,
  `BoxPriceTTC` decimal(15,2) NOT NULL,
  `BoxBuyingPrice` decimal(15,2) NOT NULL,
  `TVA` decimal(15,2) NOT NULL,
  `Description` text,
  `ImageLink` varchar(1000) DEFAULT NULL,
  `BoxStockQuantity` int NOT NULL,
  `UnitStockQuantity` int NOT NULL,
  `BoxVirtualQuantity` int NOT NULL,
  `UnitVirtualQuantity` int NOT NULL,
  `BoxMinQuantity` int NOT NULL,
  `BoxOptimalQuantity` int NOT NULL,
  `BottleQuantityPerBox` int NOT NULL,
  `IDSupplier` int NOT NULL,
  `IDWineFamily` int NOT NULL,
  PRIMARY KEY (`IDArticle`),
  KEY `IDSupplier` (`IDSupplier`),
  KEY `IDWineFamily` (`IDWineFamily`),
  CONSTRAINT `article_ibfk_1` FOREIGN KEY (`IDSupplier`) REFERENCES `supplier` (`IDSupplier`),
  CONSTRAINT `article_ibfk_2` FOREIGN KEY (`IDWineFamily`) REFERENCES `winefamily` (`IDWineFamily`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article`
--

LOCK TABLES `article` WRITE;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` VALUES (1,'TAR_FT01_2021','Domaine de Tariquet',2021,8.00,40.00,25.50,20.00,'Chaque gorgée dégustée est une véritable explosion de fruits (d\'agrumes), accompagnée d\'une agréable fraîcheur qui fera appel à une nouvelle gorgée. Le conseil de nos sommeliers : avoir toujours une bouteille au frais... au cas où !','http://www.tariquet.com/images-vins/produits/normal/ugniblanc-g.jpg',20,3,20,3,10,50,6,1,1),(2,'PEL_316_ROU','Domaine de Pellehaut',2019,6.00,40.00,20.00,20.00,'En bouche, un vin gourmand guidé par une trame tannique subtile et bien intégrée. Le fruit s\'exprime ; baies noires et mûres accompagnées de parfums poivrés.','https://www.comtessedubarry.com/media/catalog/product/p/h/phar08_2_6_1_2.jpg?quality=80&bg-color=255,255,255&fit=bounds&height=430&width=430&canvas=430:430',25,3,25,3,5,20,6,2,1),(3,'PEL_204_ROS','Domaine de Pellehaut',2021,6.00,40.00,20.00,20.00,'D\'une belle oculeur dynamique, ce rosé offre un nez d\'agrumes et de fruits rouges. En bouche, le sucre ne faiblit pas et est porté par la fraîcheur du fruit. La framboise, la pêche et le pamplemousse enrobés de douceur, chatouillent le nez et caressent les papilles.. A consommer frais à tout moment!!','https://www.vinatis.com/66052-detail_default/l-ete-gascon-rose-2021-domaine-pellehaut.png',13,1,13,1,5,20,6,2,3),(4,'PEL_456_BLA','Domaine de Pellehaut',2020,7.00,44.00,22.00,20.00,'Dans une robe limpide et brillante, le vin offre un nez plein de senteurs sur des notes de fleurs, de fruits mûrs et de fruits confits. L\'attaque est franche pour une bouche souple et suave, très équilibrée et persistante. ','https://www.vinatis.com/74692-detail_default/harmonie-de-gascogne-blanc-2022-domaine-pellehaut.png',16,3,16,3,5,20,6,2,2),(5,'UBY_789_ROU','Uby n°7',2020,8.00,46.00,23.00,20.00,'\"La mûre et la myrtille sont les arômes qui mènent la danse. La bouche est souple, gourmande et équilibrée.\"','https://images.vivino.com/thumbs/cylQsTUqRp6TSmMq15Ja9w_pb_x600.png',6,7,6,7,5,20,6,5,1),(6,'UBY_635_BLA','Uby n°4',2021,7.00,32.00,18.00,20.00,'\"La mûre et la myrtille sont les arômes qui mènent la danse. La bouche est souple, gourmande et équilibrée.\"','https://www.vinatis.com/64274-detail_default/gros-et-petit-manseng-doux-n4-2021-domaine-uby.png',8,0,8,0,5,20,6,5,2),(7,'UBY_415_ROS','Uby n°6',2022,7.00,31.00,21.00,20.00,'Avec ses arômes de framboise et de fraise présents aussi bien au nez qu\'en bouche, ce rosé est plus que gourmand. C\'est un véritable délice de fraîcheur qui s\'abattra sur vos apéritifs et barbecues.','https://www.vinatis.com/76194-detail_default/uby-rose-n6-2022-domaine-uby.png',7,3,7,3,5,20,6,5,3),(8,'TAR_126_ROS','Domaine de Tariquet',2021,7.00,41.00,33.00,20.00,'Des arômes de framboises sauvages et de fleurs d\'été pour ce Tariquet rosé frais et généreux.\nUn très bon rosé donc, sans prétention mais avec un super rapport qualité/prix, et la capsule à vis le rend très pratique!','https://www.vinsolite.fr/1763-large_default/tariquet-rose.jpg',17,3,17,3,5,20,6,3,1),(9,'TAR_478_BLA','Domaine de Tariquet',2021,7.00,34.00,17.00,20.00,'Les Premières Grives ou le plaisir de se faire plaisir... ! Succès mondial, les \" Premières Grives \" du Domaine du Tariquet c\'est avant tout : \"une bouche gourmande, fruitée , vive et moelleuse.','https://www.vinatis.com/64729-detail_default/premieres-grives-2021-domaine-tariquet.png',17,1,17,1,5,20,6,1,2),(10,'FON_782_ROU','Maubet rosé',2021,6.00,36.00,18.00,20.00,'Gascon de souche, sa forte identité contribue à la personnalité des rouges, haute en couleur où se conjuguent noble astringence et fermeté tannique. Ses arômes sont ceux de fruits rouges (mûre, cassis) et de fruits confits','https://maisonfontan.com/resource/doc/vins/Incontournables/MaubetRose.jpg',9,7,9,7,5,20,6,4,3),(11,'FON_896_BLA','Maubet blanc sec',2021,6.00,36.00,18.00,20.00,'Cépage autochtone développé dans les années 80, période où les Côtes de Gascogne ont commencé à être connus. Réputé pour ses notes fraîches, relevées et toniques de fruits exotiques, fruits de la passion et agrumes.','https://maisonfontan.com/resource/doc/vins/Incontournables/MaubetBlancSec.jpg',8,3,8,3,5,20,6,4,2),(12,'FON_482_ROU','Maubet rouge',2021,6.00,27.00,13.00,20.00,'Gascon de souche, sa forte identité contribue à la personnalité des rouges, haute en couleur où se conjuguent noble astringence et fermeté tannique. Ses arômes sont ceux de fruits rouges (mûre, cassis) et de fruits confits.','https://maisonfontan.com/resource/doc/vins/Incontournables/MaubetRouge.jpg',9,0,9,0,5,20,6,4,1),(13,'JOY_693_ROU','Naturellement Joy',2020,6.00,37.00,16.00,20.00,'Les cuvées \"Naturellement Joÿ\" sont des vins équilibrés et harmonieux en conversion vers l\'agriculture biologique. Ce millésime 2020 offre une belle robe rouge avec un nez de bourgeon de cassis et de framboise.','https://www.vinatis.com/65161-detail_default/naturellement-joy-rouge-2020-domaine-de-joy.png',27,3,27,3,5,20,6,3,1),(14,'JOY_693_ROS','Saint André',2021,6.00,38.00,18.00,20.00,'Le Saint-André possède une belle robe jaune aux reflets dorés. Le nez est explosif sur des notes de fruits exotiques (ananas, mangue, litchi). En bouche, l\'équilibre entre le sucre et l\'acidité est parfait, rendant ce vin frais, aérien et gourmand.','https://www.vinatis.com/70140-detail_default/saint-andre-2021-domaine-de-joy.png',35,3,35,3,5,20,6,3,3),(15,'JOY_741_BLA','Saint André',2021,7.00,37.00,17.00,20.00,'Le Saint-André possède une belle robe jaune aux reflets dorés. Le nez est explosif sur des notes de fruits exotiques (ananas, mangue, litchi). En bouche, l\'équilibre entre le sucre et l\'acidité est parfait, rendant ce vin frais, aérien et gourmand. ','https://www.vinatis.com/75875-detail_default/saint-andre-2022-domaine-de-joy.png',13,7,13,7,5,20,6,3,2);
/*!40000 ALTER TABLE `article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `IDClient` int NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(256) NOT NULL,
  `Lastname` varchar(256) NOT NULL,
  `Address` varchar(256) NOT NULL,
  `PostalCode` varchar(40) NOT NULL,
  `Town` varchar(256) NOT NULL,
  `Country` varchar(256) NOT NULL,
  `Email` varchar(256) NOT NULL,
  `Password` varchar(256) NOT NULL,
  PRIMARY KEY (`IDClient`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'Michel','Dupont','45 Rue des Pommiers','75000','Paris','France','michel.dupont@gmail.com','1234'),(2,'Catherine','Dupont','45 Rue des Poiriers','29200','Brest','France','catherine.dupont@gmail.com','catcat'),(3,'José','Bertrand','12 Rue de la Mairie','44560','Soudan','France','jose.bertrand@gmail.com','1234'),(4,'Jeanne','Petit','11 Rue de la Poste','07110','Joyeuse','France','jeanne.petit@gmail.com','jeanne');
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientcommand`
--

DROP TABLE IF EXISTS `clientcommand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientcommand` (
  `IDClientCommand` int NOT NULL AUTO_INCREMENT,
  `CommandDate` datetime NOT NULL,
  `TotalCostTTC` decimal(15,2) NOT NULL,
  `IDClient` int NOT NULL,
  `IDCommandStatus` int NOT NULL,
  `TotalCostHT` decimal(15,2) NOT NULL,
  PRIMARY KEY (`IDClientCommand`),
  KEY `IDClient` (`IDClient`),
  KEY `IDCommandStatus` (`IDCommandStatus`),
  CONSTRAINT `clientcommand_ibfk_1` FOREIGN KEY (`IDClient`) REFERENCES `client` (`IDClient`),
  CONSTRAINT `clientcommand_ibfk_2` FOREIGN KEY (`IDCommandStatus`) REFERENCES `commandstatus` (`IDCommandStatus`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientcommand`
--

LOCK TABLES `clientcommand` WRITE;
/*!40000 ALTER TABLE `clientcommand` DISABLE KEYS */;
/*!40000 ALTER TABLE `clientcommand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientcommandlist`
--

DROP TABLE IF EXISTS `clientcommandlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientcommandlist` (
  `IDArticle` int NOT NULL,
  `IDClientCommand` int NOT NULL,
  `Quantity` int NOT NULL,
  `IDQuantityType` int NOT NULL,
  PRIMARY KEY (`IDArticle`,`IDClientCommand`),
  KEY `IDClientCommand` (`IDClientCommand`),
  CONSTRAINT `clientcommandlist_ibfk_1` FOREIGN KEY (`IDArticle`) REFERENCES `article` (`IDArticle`),
  CONSTRAINT `clientcommandlist_ibfk_2` FOREIGN KEY (`IDClientCommand`) REFERENCES `clientcommand` (`IDClientCommand`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientcommandlist`
--

LOCK TABLES `clientcommandlist` WRITE;
/*!40000 ALTER TABLE `clientcommandlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `clientcommandlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commandstatus`
--

DROP TABLE IF EXISTS `commandstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commandstatus` (
  `IDCommandStatus` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDCommandStatus`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandstatus`
--

LOCK TABLES `commandstatus` WRITE;
/*!40000 ALTER TABLE `commandstatus` DISABLE KEYS */;
INSERT INTO `commandstatus` VALUES (1,'CLOS'),(2,'EN COURS');
/*!40000 ALTER TABLE `commandstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commandtype`
--

DROP TABLE IF EXISTS `commandtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commandtype` (
  `IDCommandType` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDCommandType`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandtype`
--

LOCK TABLES `commandtype` WRITE;
/*!40000 ALTER TABLE `commandtype` DISABLE KEYS */;
INSERT INTO `commandtype` VALUES (1,'MANUEL'),(2,'AUTOMATIQUE');
/*!40000 ALTER TABLE `commandtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `datautils`
--

DROP TABLE IF EXISTS `datautils`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `datautils` (
  `DataKey` varchar(256) NOT NULL,
  `DataValue` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`DataKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datautils`
--

LOCK TABLES `datautils` WRITE;
/*!40000 ALTER TABLE `datautils` DISABLE KEYS */;
INSERT INTO `datautils` VALUES ('defaultmargin','30'),('defaulttva','20'),('password',NULL),('username',NULL);
/*!40000 ALTER TABLE `datautils` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `quantitytype`
--

DROP TABLE IF EXISTS `quantitytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quantitytype` (
  `IDQuantityType` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDQuantityType`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `quantitytype`
--

LOCK TABLES `quantitytype` WRITE;
/*!40000 ALTER TABLE `quantitytype` DISABLE KEYS */;
INSERT INTO `quantitytype` VALUES (1,'UNITE'),(2,'CARTON');
/*!40000 ALTER TABLE `quantitytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier` (
  `IDSupplier` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  `Address` varchar(256) NOT NULL,
  `PostalCode` varchar(40) NOT NULL,
  `Town` varchar(256) NOT NULL,
  `Country` varchar(256) NOT NULL,
  `Email` varchar(256) NOT NULL,
  PRIMARY KEY (`IDSupplier`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` VALUES (1,' Domaine de Tariquet','Château du Tariquet','32800','Eauze','France','contact@domaine-tariquet.fr'),(2,'Domaine de Pellehaut','Pellehaut','32250','Montréal','France','domaine-pellehaut@gmail.com'),(3,'Joy','Joy','32110','Panjas','France','domaine-joy@gmail.com'),(4,'Maison Fontan','Maubet','32800','Noulens','France','maison-fontan@yahoo.fr'),(5,'Uby','Uby','32150','Cazauban','France','vignoble-uby@gmail.com');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliercommand`
--

DROP TABLE IF EXISTS `suppliercommand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliercommand` (
  `IDSupplierCommand` int NOT NULL AUTO_INCREMENT,
  `TransportCost` decimal(15,2) NOT NULL,
  `CommandDate` datetime NOT NULL,
  `TotalCost` decimal(15,2) NOT NULL,
  `IDSupplier` int NOT NULL,
  `IDCommandStatus` int NOT NULL,
  `IDCommandType` int NOT NULL,
  PRIMARY KEY (`IDSupplierCommand`),
  KEY `IDSupplier` (`IDSupplier`),
  KEY `IDCommandStatus` (`IDCommandStatus`),
  KEY `IDCommandType` (`IDCommandType`),
  CONSTRAINT `suppliercommand_ibfk_1` FOREIGN KEY (`IDSupplier`) REFERENCES `supplier` (`IDSupplier`),
  CONSTRAINT `suppliercommand_ibfk_2` FOREIGN KEY (`IDCommandStatus`) REFERENCES `commandstatus` (`IDCommandStatus`),
  CONSTRAINT `suppliercommand_ibfk_3` FOREIGN KEY (`IDCommandType`) REFERENCES `commandtype` (`IDCommandType`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliercommand`
--

LOCK TABLES `suppliercommand` WRITE;
/*!40000 ALTER TABLE `suppliercommand` DISABLE KEYS */;
INSERT INTO `suppliercommand` VALUES (1,0.00,'2023-02-14 11:15:21',96.00,3,2,1),(2,0.00,'2023-02-14 11:16:39',168.00,3,1,1);
/*!40000 ALTER TABLE `suppliercommand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliercommandlist`
--

DROP TABLE IF EXISTS `suppliercommandlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliercommandlist` (
  `IDArticle` int NOT NULL,
  `IDSupplierCommand` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`IDArticle`,`IDSupplierCommand`),
  KEY `IDSupplierCommand` (`IDSupplierCommand`),
  CONSTRAINT `suppliercommandlist_ibfk_1` FOREIGN KEY (`IDArticle`) REFERENCES `article` (`IDArticle`),
  CONSTRAINT `suppliercommandlist_ibfk_2` FOREIGN KEY (`IDSupplierCommand`) REFERENCES `suppliercommand` (`IDSupplierCommand`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliercommandlist`
--

LOCK TABLES `suppliercommandlist` WRITE;
/*!40000 ALTER TABLE `suppliercommandlist` DISABLE KEYS */;
INSERT INTO `suppliercommandlist` VALUES (13,1,6),(13,2,6),(14,2,4);
/*!40000 ALTER TABLE `suppliercommandlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `winefamily`
--

DROP TABLE IF EXISTS `winefamily`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `winefamily` (
  `IDWineFamily` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDWineFamily`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `winefamily`
--

LOCK TABLES `winefamily` WRITE;
/*!40000 ALTER TABLE `winefamily` DISABLE KEYS */;
INSERT INTO `winefamily` VALUES (2,'Blanc'),(5,'Digestif'),(4,'Pétillant'),(3,'Rosé'),(1,'Rouge');
/*!40000 ALTER TABLE `winefamily` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-02-15 10:39:24
