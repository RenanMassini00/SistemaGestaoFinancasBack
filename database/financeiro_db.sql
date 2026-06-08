CREATE DATABASE IF NOT EXISTS financeiro_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE financeiro_db;

DROP TABLE IF EXISTS transacoes;
DROP TABLE IF EXISTS usuarios;

CREATE TABLE usuarios (
    id CHAR(36) NOT NULL,
    nome VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL,
    password_hash VARCHAR(128) NOT NULL,
    role VARCHAR(50) NOT NULL,
    ativo TINYINT(1) NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    CONSTRAINT PK_usuarios PRIMARY KEY (id),
    CONSTRAINT UQ_usuarios_email UNIQUE (email)
) ENGINE=InnoDB;

CREATE TABLE transacoes (
    id CHAR(36) NOT NULL,
    descricao VARCHAR(200) NOT NULL,
    valor DECIMAL(18,2) NOT NULL,
    data_lancamento DATETIME NOT NULL,
    tipo INT NOT NULL,
    categoria VARCHAR(120) NOT NULL,
    status VARCHAR(80) NOT NULL,
    conciliado TINYINT(1) NOT NULL DEFAULT 0,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    CONSTRAINT PK_transacoes PRIMARY KEY (id)
) ENGINE=InnoDB;

INSERT INTO usuarios (id, nome, email, password_hash, role, ativo)
VALUES
('e052eb27-92e4-4319-b61f-1ff22ac66c3d', 'Administrador', 'admin@massinilabs.local', 'e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7', 'Admin', 1);

INSERT INTO transacoes (id, descricao, valor, data_lancamento, tipo, categoria, status, conciliado)
VALUES
('e64ef4ab-842f-44d1-995e-3cc717bb0118', 'Recebimento Cliente Alpha', 8500.00, '2026-06-02 10:00:00', 1, 'Receita', 'Conciliado', 1),
('1ca3f590-6345-426f-b989-6659c8369215', 'Pagamento Fornecedor Cloud', 1290.00, '2026-06-03 14:00:00', 2, 'Infraestrutura', 'Pendente', 0);
