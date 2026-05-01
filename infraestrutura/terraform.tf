terraform {
  backend "s3" {
    bucket       = "meuplantao-bucket-tfstate"
    key          = "tfstate/terraform.tfstate"
    region       = "us-east-1"
    encrypt      = true
    use_lockfile = true
  }
}