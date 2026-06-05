resource "aws_security_group" "meuplantao_sg" {
    name = "meuplantao-sg"
    vpc_id = "ID_SUA_VPC"

    tags = {
        Name = "meuplantao-sg"
        Provisioned = "Terraform"
        Cliente = "Kauan"
    }
}

resource "aws_vpc_grup_ingress_rule" "permitir_http" {
    security_group_id = aws_security_group.meuplantao_sg.id
    cidr_ipv4 = "SEU_IP"
    from_port = 80 
    ip_protocol = "tcp"
    to_port = 80
}

resource "aws_vpc_security_group_egress_rule" "allow_all_outbound" {
  security_group_id = aws_security_group.meuplantao_sg.id 
  cidr_ipv4         = "SEU_IP"          
  ip_protocol       = "-1"                       
}
