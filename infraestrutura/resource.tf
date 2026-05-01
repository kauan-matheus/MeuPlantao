resource "aws_s3_bucket" "meuplantao-bucket" {
  bucket = "meuplantao-bucket"

  tags = {
    Name        = "meuplantao-bucket"
    Environment = "Development"
  }
}