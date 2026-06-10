CREATE DATABASE IF NOT EXISTS controle_financeiro_simples CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE controle_financeiro_simples;
DROP TABLE IF EXISTS despesas_periodo;
DROP TABLE IF EXISTS periodos_pagamento;
DROP TABLE IF EXISTS usuarios;
CREATE TABLE usuarios (
  id CHAR(36) PRIMARY KEY,
  nome VARCHAR(150) NOT NULL,
  email VARCHAR(150) NOT NULL UNIQUE,
  password_hash VARCHAR(128) NOT NULL,
  role VARCHAR(50) NOT NULL,
  ativo TINYINT(1) NOT NULL DEFAULT 1,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME NULL
) ENGINE=InnoDB;
CREATE TABLE periodos_pagamento (
  id CHAR(36) PRIMARY KEY,
  mes_referencia VARCHAR(7) NOT NULL,
  dia_pagamento INT NOT NULL,
  pessoa VARCHAR(120) NOT NULL,
  salario DECIMAL(18,2) NOT NULL,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME NULL
) ENGINE=InnoDB;
CREATE TABLE despesas_periodo (
  id CHAR(36) PRIMARY KEY,
  periodo_pagamento_id CHAR(36) NOT NULL,
  descricao VARCHAR(150) NOT NULL,
  valor DECIMAL(18,2) NOT NULL,
  tipo VARCHAR(50) NOT NULL,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME NULL,
  CONSTRAINT FK_despesas_periodo_periodos FOREIGN KEY (periodo_pagamento_id) REFERENCES periodos_pagamento(id) ON DELETE CASCADE
) ENGINE=InnoDB;
INSERT INTO usuarios (id,nome,email,password_hash,role,ativo) VALUES ('4b43c61e-8d1c-4b63-bac7-c12d1ebdbcbd','Administrador','admin@massinilabs.local','e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7','Admin',1);
SET @p1 = 'ffd606e7-1154-4367-82a4-f3819fea993e';
SET @p2 = '8b6d16b5-d3ec-4e21-9d19-0eb1ef847a0e';
INSERT INTO periodos_pagamento (id,mes_referencia,dia_pagamento,pessoa,salario) VALUES
(@p1,'2026-06',5,'Ingrid',1667.00),(@p2,'2026-06',15,'Renan',2600.00);
INSERT INTO despesas_periodo (id,periodo_pagamento_id,descricao,valor,tipo) VALUES
('01f68fe8-ec4d-415e-ab24-55276a37f033',@p1,'Condomínio',696.00,'Fixa'),
('1be321a0-47ea-4806-a9d1-ad0f4eebbc87',@p1,'ENEL',174.00,'Fixa'),
('c00db904-0b7f-4524-bb2d-7dce71c2f259',@p1,'Internet',125.90,'Fixa'),
('dd888803-33c8-414b-8614-9660151d5551',@p2,'Cartão Nubank',1573.34,'Cartao'),
('b2af0b55-3ac8-4d70-84c4-f09318e06153',@p2,'Sogro',1118.98,'Fixa');
