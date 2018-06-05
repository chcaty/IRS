<template>
  <el-container style="height:600px">
    <el-aside :span="8">
      <div style="position:fixed;left:-3%;top:18%;">
        <img style="vertical-align: middle" src="static/img/logo.png">
        <el-row>
          <el-button type="info" icon="el-icon-search" @click="search()">搜索报修记录</el-button>
        </el-row>
        <el-row>
          <el-button type="info" @click="add()" icon="el-icon-plus">新增报修记录</el-button>
        </el-row>
      </div>
    </el-aside>
    <el-container>
      <el-header v-show="isSearch">
        <el-col :offset="2">
          <el-form id="toolbar" :inline="true" :model="searchForm" class="demo-form-inline">
            <el-form-item label="姓名" :xs="20" :sm="11" :md="7" :lg="6" :xl="2">
              <el-input v-model="searchForm.RecordUser" placeholder="用户姓名"></el-input>
            </el-form-item>
            <el-form-item label="宿舍号" :xs="20" :sm="11" :md="7" :lg="6" :xl="2">
              <el-input v-model="searchForm.UserDorm" placeholder="宿舍号"></el-input>
            </el-form-item>
            <el-form-item :xs="20" :sm="11" :md="7" :lg="6" :xl="2">
              <el-button @click="find()" plain>查询</el-button>
              <el-button type="info" @click="findReset()" plain>重置</el-button>
            </el-form-item>
          </el-form>
        </el-col>
      </el-header>
      <el-main :span="16">
        <el-col :xs="20" :sm="11" :md="7" :lg="6" :xl="2" v-for="(data,index) in tableData" :key="index" :offset="1">
          <el-card :body-style="{ padding: '10px',margin:'10px' }" center=true shadow="hover">
            <p>宿舍:
              <span>{{data.dormCategoryName}}{{data.userDorm}}</span>
            </p>
            <p>故障类型:{{data.faultCategoryName}}</p>
            <p>故障描述:</p>
            <p>{{data.faultDesc}}</p>
            <div style="padding: 10px;">
              <p>报修人:{{data.recordUser}}</p>
              <div class="bottom clearfix">
                <time class="time">{{data.recordTime}}</time>
                <el-button type="text" class="button" @click="detail(data.recordId)">查看详情</el-button>
              </div>
            </div>
          </el-card>
        </el-col>
        <v-goTop></v-goTop>
      </el-main>
    </el-container>
    <el-dialog title="报修信息" :visible.sync="editDialogFormVisible" @close="cancel(); ">
      <el-form ref="form" :model="form" :rules="rules" label-width="80px" label-position="top">
        <el-row class="first-row">
          <el-col :span="10" class="first-column" :offset="2">
            <el-form-item label="报修人" required prop="recordUser">
              <el-input v-model="form.recordUser" placeholder=''></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="10">
            <el-form-item label="联系电话" required prop="userPhone">
              <el-input v-model="form.userPhone" placeholder=''></el-input>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row class="normal-row">
          <el-col :span="10" class="first-column" :offset="2">
            <el-form-item label="宿舍楼" required prop="dormCategory">
              <template>
                <el-select v-model="form.dormCategory" placeholder="请选择">
                  <el-option v-for="item in dormList" :key="item.categoryInfoId" :label="item.categoryInfoName" :value="item.categoryInfoId">
                    <span style="float: left">{{ item.categoryInfoName }}</span>
                  </el-option>
                </el-select>
              </template>
            </el-form-item>
          </el-col>
          <el-col :span="10">
            <el-form-item label="宿舍号" required prop="userDorm">
              <el-input v-model="form.userDorm"></el-input>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row class="last-row" style="height: 200px; overflow: hidden">
          <el-col :span="10" class="first-column" :offset="2" style="height: 100%">
            <el-form-item label="故障分类" required prop="faultCategory">
              <template>
                <el-select v-model="form.faultCategory" placeholder="请选择">
                  <el-option v-for="item in faultList" :key="item.categoryInfoId" :label="item.categoryInfoName" :value="item.categoryInfoId">
                    <span style="float: left">{{ item.categoryInfoName }}</span>
                  </el-option>
                </el-select>
              </template>
            </el-form-item>
          </el-col>
          <el-col :span="10" style="height: 100%">
            <el-form-item label="故障描述" required prop="faultDesc">
              <el-input type="textarea" :autosize="{ minRows: 2, maxRows: 4}" v-model="form.faultDesc"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>

      <div slot="footer" class="dialog-footer">
        <el-button @click="cancel()">取 消</el-button>
        <el-button type="primary" @click="save('form')">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog title="处理信息" :visible.sync="processDialogFormVisible" @close="cancel(); ">
      <el-card :body-style="{ padding: '10px',margin:'10px'}" center=true shadow="hover">
        <p>报修人:{{form.recordUser}}</p>
        <p>报修时间:{{form.recordTime}}</p>
        <p>宿舍:
          <span>{{form.dormCategoryName}}{{form.userDorm}}</span>
        </p>
        <p>故障类型:{{form.faultCategoryName}}</p>
        <p>处理结果:{{form.faultResultName}}</p>
        <p>故障描述:</p>
        <p>{{form.faultDesc}}</p>
        <div style="padding: 10px;">
          <p>处理记录</p>
          <div class="bottom clearfix" v-for="(data,index) in processData" :key="index">
            <p>
              <time class="time">{{data.processingTime}} {{data.processingDesc}} {{data.processingResultName}} {{data.processingPeople}} </time>
            </p>
          </div>
        </div>
      </el-card>
    </el-dialog>
  </el-container>
</template>

<script>
import goTop from "@/views/components/goTop";
import {
  getAllInfo,
  getInfoById,
  addInfo,
  getMenuByType,
  SearchModel,
  getRecordById,
  Model,
  ProcessModel
} from "@/api/record";

var validPhone=(rule, value,callback)=>{
      if (!value){
          callback(new Error('请输入电话号码'))
      }else  if (!isvalidPhone(value)){
        callback(new Error('请输入正确的11位手机号码'))
      }else {
          callback()
      }
  }

export function isvalidPhone(str) {
  const reg = /^1[3|4|5|7|8][0-9]\d{8}$/
  return reg.test(str)
}

export default {
  data() {
    return {
      searchForm: new SearchModel(),
      form: new Model(),
      processForm: new ProcessModel(),
      currentDate: new Date(),
      form: new Model(),
      editDialogFormVisible: false,
      processDialogFormVisible:false,
      tableData: [],
      processData:[],
      faultList:[],
      isSearch:false,
      dormList:[],
      rules: {
          recordUser: [
            { required: true, message: '请输入报修人名称', trigger: 'blur' }
          ],
          userPhone: [
            { required: true, message: '请输入正确的联系电话', validator: validPhone, trigger: 'blur' }
          ],
          dormCategory: [
            { required: true, message: '请选择宿舍楼', trigger: 'change' }
          ],
          userDorm: [
            { required: true, message: '请输入宿舍号', trigger: 'blur' }
          ],
          faultCategory: [
           { required: true, message: '请选择故障类型', trigger: 'change' }
          ],
          faultDesc: [
             { required: true, message: '请输入故障描述', trigger: 'blur' }
          ]
      }
    }
  },
  methods: {
    backTop() {
      var currentScroll = document.documentElement.scrollTop || document.body.scrollTop;
      if (currentScroll > 0) {
        window.requestAnimationFrame(smoothscroll);
        window.scrollTo (0,currentScroll - (currentScroll/5));
      }
    },
    add(){
       this.form = new Model()
       this.editDialogFormVisible = true
    },
    detail(row){
      let id = row;
      getInfoById(id).then(response => {
        let result = response.data;
        this.form = result;
      });
      getRecordById(id).then(response =>{
        let result = response.data;
        this.processData = result;
        this.processDialogFormVisible = true;
      })
    },
    search(){
      if(!this.isSearch){
        this.searchForm = new SearchModel();
      }
      this.isSearch = !this.isSearch;
    },
    cancel() {
       this.editDialogFormVisible = false;
    },
    save(formName) {
      this.$refs[formName].validate((valid) =>{
        if(valid){
          this.editDialogFormVisible = false;
          addInfo(this.form).then(response => {
            Tools.success(this, '报修记录添加成功')
            this.fetchData();
          }).catch(err => {
            this.fetchData();
          })
        }
      });
    },
    find(){
       this.fetchData()
    },
    findReset() {
       this.searchForm = new SearchModel()
       this.fetchData()
    },
    fetchData(searchObj = this.searchForm) {
       this.loading = true
       getAllInfo(searchObj).then(response => {
          let result = response.data;
          this.tableData = result;
        })
        .catch(() => {
        });
    },
  },
  components:{
    'v-goTop':goTop
  },
  beforeCreate() {
    getMenuByType(2).then(res =>{
      this.faultList = res.data;
    })
    getMenuByType(1).then(res => {
      this.dormList = res.data;
      this.fetchData()
    }).catch(err => {
    })
  }
}
</script>

<style>
p {
    font-size: 14px;
}
img {
    width: 100%;
    height: auto;
    max-width: 100%;
    display: block;
}
.el-aside {
    background-color: #909399;
    color: #333;
    text-align: center;
}
.el-container {
    background-color: #e9ecf3;
}
.time {
    font-size: 13px;
    color: #999;
}

.bottom {
    margin-top: 13px;
    line-height: 12px;
}
.back_to_top {
    position: fixed;
    bottom: 30px;
    right: 30px;
    border: 1px solid #888;
}
.button {
    padding: 0;
    float: right;
}

.clearfix:before,
.clearfix:after {
    display: table;
    content: "";
}

.clearfix:after {
    clear: both;
}
.el-card {
    margin: 10px;
}
</style>

