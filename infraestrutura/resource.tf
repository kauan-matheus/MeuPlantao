resource "aws_s3_bucket" "meuplantao-bucket" {
  bucket = "meuplantao-bucket"

  tags = {
    Name        = "meuplantao-bucket"
    Environment = "Development"
  }
}

resource "aws_instance" "servidor-mp" {
  ami = "ami-0ec10929233384c7f"
  instance_type = "t3-small"
  key_name = "CHAVE_AWS"
  iam_instance_profile = "SUA_ROLE_IAM"

  tags = {
    Name = "servidor-mp",
    Provisioned = "Terraform",
    Cliente = "Meu Plantao"
  }
}



