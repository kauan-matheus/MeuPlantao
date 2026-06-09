-- ==========================================
-- SCRIPT DE SEED: MEU PLANTÃO V2 (VOLUME MASSIVO)
-- ==========================================
-- Este script apaga os dados atuais (exceto os 5 usuários base)
-- e recria estabelecimentos, setores, profissionais, plantões
-- e muitas solicitações pendentes para testes em tempo real.

-- 1. Limpeza de Dados
DELETE FROM public."HistoricoPlantao";
DELETE FROM public."Historico";
DELETE FROM public."TrocaPlantoes";
DELETE FROM public."Solicitacoes";
DELETE FROM public."Plantoes";
DELETE FROM public."Setores";
DELETE FROM public."Profissionais";
DELETE FROM public."Estabelecimentos";

-- 2. Inserir Estabelecimentos
INSERT INTO public."Estabelecimentos" ("Nome") VALUES 
('Hospital Israelita Albert Einstein'),
('Hospital Sírio-Libanês'),
('Hospital das Clínicas (HC)'),
('Santa Casa de Misericórdia'),
('Hospital São Luiz'),
('Hospital Moinhos de Vento'),
('Hospital Samaritano');

-- 3. Inserir Profissionais
INSERT INTO public."Profissionais" ("Nome", "Role", "Crm", "Coren", "Telefone", "UserId")
SELECT 'Dra. Maria Clara', 2, '123456', NULL, '999991111', "Id" FROM public."Usuarios" WHERE "Email" = 'maria@hospital.com';

INSERT INTO public."Profissionais" ("Nome", "Role", "Crm", "Coren", "Telefone", "UserId")
SELECT 'Dr. Carlos Eduardo', 2, '654321', NULL, '999992222', "Id" FROM public."Usuarios" WHERE "Email" = 'carlos@hospital.com';

INSERT INTO public."Profissionais" ("Nome", "Role", "Crm", "Coren", "Telefone", "UserId")
SELECT 'Enf. Ana Beatriz', 2, NULL, '112233', '999993333', "Id" FROM public."Usuarios" WHERE "Email" = 'ana@hospital.com';

-- 4. Inserir Setores (Diferentes administradores para testes diversos)
-- admin@hospital.com
INSERT INTO public."Setores" ("Nome", "RepresentanteId", "EstabelecimentoId")
SELECT 'UTI Adulto', u."Id", e."Id" FROM public."Usuarios" u, public."Estabelecimentos" e WHERE u."Email" = 'admin@hospital.com' AND e."Nome" = 'Hospital Israelita Albert Einstein';

INSERT INTO public."Setores" ("Nome", "RepresentanteId", "EstabelecimentoId")
SELECT 'Pronto Socorro', u."Id", e."Id" FROM public."Usuarios" u, public."Estabelecimentos" e WHERE u."Email" = 'admin@hospital.com' AND e."Nome" = 'Hospital Israelita Albert Einstein';

INSERT INTO public."Setores" ("Nome", "RepresentanteId", "EstabelecimentoId")
SELECT 'Cardiologia', u."Id", e."Id" FROM public."Usuarios" u, public."Estabelecimentos" e WHERE u."Email" = 'admin@hospital.com' AND e."Nome" = 'Hospital das Clínicas (HC)';

-- joao@hospital.com
INSERT INTO public."Setores" ("Nome", "RepresentanteId", "EstabelecimentoId")
SELECT 'Pediatria', u."Id", e."Id" FROM public."Usuarios" u, public."Estabelecimentos" e WHERE u."Email" = 'joao@hospital.com' AND e."Nome" = 'Hospital Sírio-Libanês';

INSERT INTO public."Setores" ("Nome", "RepresentanteId", "EstabelecimentoId")
SELECT 'Centro Cirúrgico', u."Id", e."Id" FROM public."Usuarios" u, public."Estabelecimentos" e WHERE u."Email" = 'joao@hospital.com' AND e."Nome" = 'Hospital Sírio-Libanês';


-- 5. Inserir Plantões APROVADOS (Status 1) - Eles já pertencem a alguém
-- Maria na UTI Adulto (15 plantões)
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", p."Id", 1250.00, NOW() + (i || ' days')::interval, NOW() + (i || ' days 12 hours')::interval, 1 
FROM public."Setores" s, public."Profissionais" p, generate_series(1, 15) as i
WHERE s."Nome" = 'UTI Adulto' AND p."Nome" = 'Dra. Maria Clara';

-- Carlos na Cardiologia (15 plantões)
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", p."Id", 1800.00, NOW() + (i || ' days')::interval, NOW() + (i || ' days 12 hours')::interval, 1 
FROM public."Setores" s, public."Profissionais" p, generate_series(1, 15) as i
WHERE s."Nome" = 'Cardiologia' AND p."Nome" = 'Dr. Carlos Eduardo';

-- Ana na Pediatria (15 plantões)
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", p."Id", 900.00, NOW() + (i || ' days')::interval, NOW() + (i || ' days 6 hours')::interval, 1 
FROM public."Setores" s, public."Profissionais" p, generate_series(1, 15) as i
WHERE s."Nome" = 'Pediatria' AND p."Nome" = 'Enf. Ana Beatriz';


-- 6. Inserir Plantões DISPONÍVEIS E PENDENTES (Status 0)
-- 60 plantões para o Pronto Socorro
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", NULL, 1100.00, NOW() + (i || ' days')::interval, NOW() + (i || ' days 8 hours')::interval, 0 
FROM public."Setores" s, generate_series(1, 60) as i
WHERE s."Nome" = 'Pronto Socorro';

-- 30 plantões para o Centro Cirúrgico
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", NULL, 2000.00, NOW() + (i || ' days')::interval, NOW() + (i || ' days 10 hours')::interval, 0 
FROM public."Setores" s, generate_series(1, 30) as i
WHERE s."Nome" = 'Centro Cirúrgico';


-- 7. Inserir Solicitações Pendentes (Isto fará com que fiquem no status "Pendente" aguardando aprovação)
-- Maria pede 15 plantões do Pronto Socorro
INSERT INTO public."Solicitacoes" ("PlantaoId", "ProfissionalId", "CreatedAt")
SELECT pl."Id", pr."Id", NOW() 
FROM public."Plantoes" pl
JOIN public."Setores" s ON pl."SetorId" = s."Id"
CROSS JOIN public."Profissionais" pr
WHERE pr."Nome" = 'Dra. Maria Clara' 
  AND s."Nome" = 'Pronto Socorro' 
  AND pl."Status" = 0
  AND pl."Id" NOT IN (SELECT "PlantaoId" FROM public."Solicitacoes")
LIMIT 15;

-- Carlos pede 15 plantões do Pronto Socorro
INSERT INTO public."Solicitacoes" ("PlantaoId", "ProfissionalId", "CreatedAt")
SELECT pl."Id", pr."Id", NOW() 
FROM public."Plantoes" pl
JOIN public."Setores" s ON pl."SetorId" = s."Id"
CROSS JOIN public."Profissionais" pr
WHERE pr."Nome" = 'Dr. Carlos Eduardo' 
  AND s."Nome" = 'Pronto Socorro' 
  AND pl."Status" = 0
  AND pl."Id" NOT IN (SELECT "PlantaoId" FROM public."Solicitacoes")
LIMIT 15;

-- Ana pede 15 plantões do Centro Cirúrgico
INSERT INTO public."Solicitacoes" ("PlantaoId", "ProfissionalId", "CreatedAt")
SELECT pl."Id", pr."Id", NOW() 
FROM public."Plantoes" pl
JOIN public."Setores" s ON pl."SetorId" = s."Id"
CROSS JOIN public."Profissionais" pr
WHERE pr."Nome" = 'Enf. Ana Beatriz' 
  AND s."Nome" = 'Centro Cirúrgico' 
  AND pl."Status" = 0
  AND pl."Id" NOT IN (SELECT "PlantaoId" FROM public."Solicitacoes")
LIMIT 15;

-- 8. Inserir Plantões RECUSADOS / CANCELADOS (Status 2)
-- Plantões que foram rejeitados no Pronto Socorro (10 plantões)
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", NULL, 1150.00, NOW() - (i || ' days')::interval, NOW() - (i || ' days 8 hours')::interval, 2 
FROM public."Setores" s, generate_series(1, 10) as i
WHERE s."Nome" = 'Pronto Socorro';

-- Plantões que foram rejeitados na Cardiologia (10 plantões)
INSERT INTO public."Plantoes" ("SetorId", "ProfissionalResponsavelId", "Valor", "Inicio", "Fim", "Status")
SELECT s."Id", NULL, 1800.00, NOW() - (i || ' days')::interval, NOW() - (i || ' days 12 hours')::interval, 2 
FROM public."Setores" s, generate_series(1, 10) as i
WHERE s."Nome" = 'Cardiologia';


-- 9. Inserir Histórico de Plantões (Log)
INSERT INTO public."HistoricoPlantao" ("PlantaoId", "Evento", "Data", "UsuarioId", "Observacao")
SELECT p."Id", 0, NOW(), s."RepresentanteId", 'Plantão gerado via Seed Massivo'
FROM public."Plantoes" p
INNER JOIN public."Setores" s ON p."SetorId" = s."Id";

-- FIM DO SCRIPT
